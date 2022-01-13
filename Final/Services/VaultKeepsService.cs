using System;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsRepository _vrepo;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vrepo)
    {
      _repo = repo;
      _vrepo = vrepo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      Vault vault = _vrepo.GetById(newVaultKeep.VaultId);
      if (vault.CreatorId != newVaultKeep.CreatorId)
      {
        throw new Exception("Invalid Request!");
      }
      _repo.updateKeeps(newVaultKeep.KeepId, 1);
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

    internal VaultKeep GetByKeepId(int id)
    {
      VaultKeep vaultKeep = _repo.GetByKeepId(id);
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
      _repo.updateKeeps(vaultKeep.KeepId, -1);
    }


  }
}