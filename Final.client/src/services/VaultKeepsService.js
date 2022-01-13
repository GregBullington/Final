import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
  async removeFromVault(id) {
    await api.delete(`api/vaultkeeps/${id}`)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.id !== id)
  }

  async addToVault(keepId, vaultId) {
    const res = await api.post('api/vaultkeeps', { keepId: keepId, vaultId: vaultId })
    logger.log(res.data)
  }

}

export const vaultKeepsService = new VaultKeepsService()