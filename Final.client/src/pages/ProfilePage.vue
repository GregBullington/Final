<template>
  <div class="about p-5">
    <div class="row">
      <div class="col-md-3">
        <img class="rounded mb-5" :src="activeProfile.picture" alt="" />
      </div>
      <div class="col-9">
        <h1 class="mb-4">{{ activeProfile.name }}</h1>
        <h3>Vaults: {{ profileVaults.length }}</h3>
        <h3 class="mb-4">Keeps: {{ profileKeeps.length }}</h3>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <h2>
          Vaults<button
            class="btn mdi mdi-plus mdi-24px text-secondary"
            data-bs-toggle="modal"
            data-bs-target="#createVault"
          ></button>
        </h2>
        <div class="row" v-if="route.params.id !== account.id">
          <div class="col-md-3" v-for="v in profileVaults" :key="v.id">
            <Vault :vault="v" />
          </div>
        </div>
        <div class="row" v-else>
          <div class="col-md-3" v-for="v in accountVaults" :key="v.id">
            <Vault :vault="v" />
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <h2>
          Keeps<button
            class="btn mdi mdi-plus mdi-24px text-secondary"
            data-bs-toggle="modal"
            data-bs-target="#createKeep"
          ></button>
        </h2>
        <div class="row">
          <div class="col-md-2" v-for="k in profileKeeps" :key="k.id">
            <Keep :keep="k" />
          </div>
        </div>
      </div>
    </div>
  </div>
  <button
    v-if="myProfile.id === activeProfile.id"
    class="mdi mdi-account floating-btn-right"
    data-bs-toggle="offcanvas"
    data-bs-target="#offcanvasRight"
    aria-controls="offcanvasRight"
  ></button>
  <AccountOffCanvas />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
import { profilesService } from "../services/ProfilesService"
import { accountService } from "../services/AccountService"
export default {
  name: 'Account',
  setup() {
    const account = AppState.account
    const route = useRoute()
    onMounted(async () => {
      try {
        if (route.params.id === account.id) {
          await accountService.getAccountVaults()
        } else {
          await profilesService.getProfileVaults(route.params.id)
        }
        await profilesService.setActiveProfile(route.params.id)
        await profilesService.getProfileKeeps(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast("Something went wrong!", 'error')
      }
    })
    return {
      route,
      accountVaults: computed(() => AppState.accountVaults),
      account: computed(() => AppState.account),
      myProfile: computed(() => AppState.myProfile),
      user: computed(() => AppState.user),
      activeProfile: computed(() => AppState.activeProfile),
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults)

    }
  }
}
</script>

<style scoped>
img {
  width: 200px;
  height: 200px;
}

.mdi-plus {
  text-shadow: 1px 1px #000000;
}
</style>
