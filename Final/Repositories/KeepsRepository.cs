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
      }, new { id }).ToList();
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
      }, new { id }).FirstOrDefault();
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

    //  FIXME creator needs to be populated instead of vault
    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT 
        k.*,
        vk.id AS vaultKeepId,
        k.*
      FROM vaultKeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN vaults v ON v.id = vk.vaultId
      Where vk.vaultId = @id
      ;";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vaultKeep, prof) =>
      {
        vaultKeep.Creator = prof;
        return vaultKeep;
      }, new { id }).ToList();
    }
  }
}