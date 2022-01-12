import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from "../utils/Pop"
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }

  }
  async getMyProfile(account) {
    try {
      const res = await api.get(`api/profiles/${account.id}`)
      AppState.myProfile = res.data
    } catch (error) {
      logger.error(error)
      Pop.toast("Could not get Profile!", 'error')
    }
  }

  async edit(update) {
    try {
      const res = await api.put('/account', update)
      AppState.account = res.data
    } catch (error) {
      logger.error(error)
      Pop.toast("Could not update!", 'error')
    }

  }
}

export const accountService = new AccountService()
