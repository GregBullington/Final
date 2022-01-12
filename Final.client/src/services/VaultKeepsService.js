import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
  async removeFromVault(id) {
    await api.delete(`api/vaultkeeps/${id}`)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.id !== id)
  }

}

export const vaultKeepsService = new VaultKeepsService()