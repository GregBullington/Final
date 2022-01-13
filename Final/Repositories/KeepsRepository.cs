using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Final.Models;

namespace Final.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Keep> GetByCreatorId(string id)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.creatorId = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }, splitOn: "id").ToList();
    }

    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img, views, shares, keeps)
      VALUES
      (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
        SELECT 
        k.*,
        a.*
        FROM keeps k 
        JOIN accounts a ON k.creatorId = a.id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
      {
        keep.Creator = prof;
        return keep;
      }).ToList();
    }


    internal Keep GetById(int id)
    {
      string sql = @"
      SELECT 
        k.*,
        a.* 
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }


    internal Keep Edit(Keep editKeep)
    {
      string sql = @"UPDATE keeps
      SET creatorId = @CreatorId, name = @Name, description = @Description, img = @Img
      WHERE id = @Id;";
      int rows = _db.Execute(sql, editKeep);
      if (rows <= 0)
      {
        throw new Exception("REPO Keep Edit was not successful!");
      }
      return editKeep;
    }

    internal void Remove(int id)
    {
      string sql =
      @"DELETE FROM keeps WHERE id = @Id LIMIT 1;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("Invalid Id!");
      }
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT 
        k.*,
        vk.id AS vaultKeepId,
        a.*
      FROM vaultKeeps vk
      JOIN keeps k ON vk.keepId = k.id
      JOIN accounts a ON vk.creatorId = a.id
      Where vk.vaultId = @id
      ;";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vaultKeep, prof) =>
      {
        vaultKeep.Creator = prof;
        return vaultKeep;
      }, new { id }, splitOn: "id").ToList();
    }

    internal void updateViews(int id)
    {
      string sql = @"
      UPDATE keeps
      SET views = views + 1
      WHERE id = @id
      ;";
      _db.Execute(sql, new { id });
    }
  }
}