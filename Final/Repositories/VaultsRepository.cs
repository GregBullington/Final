using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Final.Models;

namespace Final.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Vault> GetByCreatorId(string id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.creatorId = @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { id }).ToList();
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (creatorId, name, description, isPrivate, img)
      VALUES
      (@CreatorId, @Name, @Description, @IsPrivate, @Img);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.Id = id;
      return newVault;
    }


    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT 
        v.*,
        a.* 
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { id }).FirstOrDefault();
    }


    internal Vault Edit(Vault editVault)
    {
      string sql = @"UPDATE vaults
      SET creatorId = @CreatorId, name = @Name, description = @Description, isPrivate = @IsPrivate, img = @Img
      WHERE id = @Id;";
      int rows = _db.Execute(sql, editVault);
      if (rows <= 0)
      {
        throw new Exception("REPO Vault Edit was not successful!");
      }
      return editVault;
    }

    internal void Remove(int id)
    {
      string sql =
      @"DELETE FROM vaults WHERE id = @Id LIMIT 1;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("Invalid Id!");
      }
    }
  }
}