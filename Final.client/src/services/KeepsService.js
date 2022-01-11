import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

  async getAllKeeps(query = '') {
    const res = await api.get(query)
    AppState.allKeeps = res.data
    logger.log(res.data)
  }

  async createKeep(keep) {
    const res = await api.post('api/keeps', keep)
    logger.log(res.data)
    AppState.allKeeps.push(res.data)
  }
}

export const keepsService = new KeepsService