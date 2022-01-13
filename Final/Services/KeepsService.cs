using System;
using System.Collections.Generic;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    internal List<Keep> GetByCreatorId(string id)
    {
      return _repo.GetByCreatorId(id);
    }

    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal List<Keep> GetAll()
    {
      return _repo.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep keep = _repo.GetById(id);
      if (keep == null)
      {
        throw new Exception("Invalid keep Id");
      }
      _repo.updateViews(id);
      keep.Views++;
      return keep;
    }

    internal Keep Edit(Keep editKeep, string userId)
    {
      Keep original = GetById(editKeep.Id);
      if (original.CreatorId != editKeep.CreatorId)
      {
        throw new Exception("You cannot edit this Keep!");
      }
      original.CreatorId = editKeep.CreatorId != null ? editKeep.CreatorId : original.CreatorId;
      original.Name = editKeep.Name != null ? editKeep.Name : original.Name;
      original.Description = editKeep.Description != null ? editKeep.Description : original.Description;
      original.Img = editKeep.Img != null ? editKeep.Img : original.Img;
      return _repo.Edit(editKeep);
    }

    internal void Remove(int id, string userId)
    {
      Keep keep = GetById(id);
      if (keep.CreatorId != userId)
      {
        throw new Exception("You cannot Delete this Keep!");
      }
      _repo.Remove(id);
    }

    internal object GetKeepsByVaultId(int id)
    {
      return _repo.GetKeepsByVaultId(id);
    }
  }
}