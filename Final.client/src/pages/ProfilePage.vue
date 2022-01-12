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
          ></button>
        </h2>
        <div class="row">
          <div class="col-md-3" v-for="v in profileVaults" :key="v.id">
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
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
import { profilesService } from "../services/ProfilesService"
export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getProfileKeeps(route.params.id)
        await profilesService.getProfileVaults(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast("Something went wrong!", 'error')
      }
    })
    return {
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
