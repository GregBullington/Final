using System;
using System.Collections.Generic;
using System.Linq;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal List<Vault> GetByAccountId(string id)
    {
      List<Vault> vaults = _repo.GetByCreatorId(id);
      return vaults;
    }
    internal List<Vault> GetByCreatorId(string id)
    {
      List<Vault> vaults = _repo.GetByCreatorId(id);
      List<Vault> filteredVaults = vaults.Where(v => v.IsPrivate == false).ToList();
      return filteredVaults;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault GetById(int id, string userId = null)
    {
      Vault vault = _repo.GetById(id);
      if (vault == null)
      {
        throw new Exception("Invalid Vault Id");
      }
      if (vault.IsPrivate && userId != vault.CreatorId)
      {
        throw new Exception("Invalid Request!");
      }
      return vault;
    }

    internal Vault Edit(Vault editVault, string userId)
    {
      Vault original = GetById(editVault.Id);
      if (original.CreatorId != editVault.CreatorId)
      {
        throw new Exception("You cannot edit this Vault!");
      }
      original.CreatorId = editVault.CreatorId != null ? editVault.CreatorId : original.CreatorId;
      original.Name = editVault.Name != null ? editVault.Name : original.Name;
      original.Description = editVault.Description != null ? editVault.Description : original.Description;
      original.IsPrivate = editVault.IsPrivate;
      original.Img = editVault.Img != null ? editVault.Img : original.Img;

      return _repo.Edit(editVault);
    }

    internal void Remove(int id, string userId)
    {
      Vault vault = GetById(id, userId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("You cannot Delete this Vault!");
      }
      _repo.Remove(id);
    }
  }
}