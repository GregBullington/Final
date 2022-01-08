using System;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }

    internal VaultKeep GetById(int id)
    {
      VaultKeep vaultKeep = _repo.GetById(id);
      if (vaultKeep == null)
      {
        throw new Exception("Invalid keep Id");
      }
      return vaultKeep;
    }

    internal void Remove(int id, string userId)
    {
      VaultKeep vaultKeep = GetById(id);
      if (vaultKeep.CreatorId != userId)
      {
        throw new Exception("You cannot Delete this Vault Keep!");
      }
      _repo.Remove(id);
    }


  }
}