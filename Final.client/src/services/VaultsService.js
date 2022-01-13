import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

  async createVault(vault) {
    const res = await api.post('api/vaults', vault)
    AppState.profileVaults.push(res.data)
  }

  async deleteVault(id) {
    await api.delete(`api/vaults/${id}`)
    AppState.profileVaults = AppState.profileVaults.filter(v => v.id !== id)
  }

  async getVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.vaultKeeps = res.data
  }

  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }


}

export const vaultsService = new VaultsService()