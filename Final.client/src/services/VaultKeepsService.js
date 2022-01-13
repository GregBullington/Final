import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
  async removeFromVault(id) {
    const res = await api.get(`api/vaultkeeps/${id}/keep`)
    const vaultKeepId = res.data.id
    await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.id !== id)
  }

  async addToVault(keepId, vaultId) {
    await api.post('api/vaultkeeps', { keepId: keepId, vaultId: vaultId })
  }

}

export const vaultKeepsService = new VaultKeepsService()