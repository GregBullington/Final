using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Final.Models;

namespace Final.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    internal void Remove(int id)
    {
      string sql =
      @"DELETE FROM vaultKeeps WHERE id = @Id LIMIT 1;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("Invalid Id!");
      }
    }


    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT FROM vaultKeeps WHERE id = @id;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal List<VaultKeep> GetByCreatorId(string id)
    {
      string sql = @"
      SELECT
      vk.*,
      a.*
      FROM vaultKeeps vk
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.creatorId = @id;";
      return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (keep, prof) =>
      {
        return keep;
      }, new { id }).ToList();
    }
  }
}