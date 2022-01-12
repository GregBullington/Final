import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

  async getAllKeeps(query = '') {
    const res = await api.get('api/keeps' + query)
    AppState.allKeeps = res.data
  }

  async createKeep(keep) {
    const res = await api.post('api/keeps', keep)
    AppState.allKeeps.push(res.data)
  }

  async deleteKeep(id) {
    await api.delete(`api/keeps/${id}`)
    AppState.profileKeeps = AppState.profileKeeps.filter(k => k.id !== id)
  }

  // async getById(id) {
  //   const res = await api.get(`api/keeps/${id}`)
  //   logger.log(res.data)
  //   AppState.reviews = res.data
  // }
}

export const keepsService = new KeepsService()