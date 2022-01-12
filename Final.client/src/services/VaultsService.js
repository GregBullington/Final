import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

  async createVault(vault) {
    const res = await api.post('api/vaults', vault)
    AppState.profileVaults.push(res.data)
    logger.log(res.data)
  }

  async deleteVault(id) {
    await api.delete(`api/vaults/${id}`)
    AppState.profileVaults = AppState.profileVaults.filter(v => v.id !== id)

  }

}

export const vaultsService = new VaultsService()