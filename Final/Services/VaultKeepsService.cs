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

    // internal void Remove(int id, string userId)
    // {
    //   VaultKeep vaultKeep = GetById(id);
    //   if (vaultKeep.CreatorId != userId)
    //   {
    //     throw new Exception("You cannot Delete this Vault Keep!");
    //   }
    //   _repo.Remove(id);
    // }

  }
}