using System;
using System.Data;
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
      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }

    internal void Remove(int id)
    {
      string sql = @"
      DELETE FROM vaultKeeps 
      WHERE id = @id;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("Invalid Id!");
      }
    }


    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT * FROM vaultKeeps 
      WHERE id = @id LIMIT 1;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal VaultKeep GetByKeepId(int id)
    {
      string sql = @"
      SELECT * FROM vaultKeeps
      WHERE keepId = @id LIMIT 1
      ;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal void updateKeeps(int id, int num)
    {
      string sql = @"
    UPDATE keeps
    SET keeps = keeps + @num
    WHERE id = @id
    ;";
      _db.Execute(sql, new { id, num });
    }

  }
}