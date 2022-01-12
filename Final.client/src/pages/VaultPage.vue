<template>
  <div class="component p-5">
    <div class="row">
      <div class="col-md-6">
        <h1>{{ vault.name }}</h1>
        <h6>Keeps: {{ vaultKeeps.length }}</h6>
      </div>
      <div class="col-md-6"></div>
    </div>
    <div class="row">
      <div class="col-md-2" v-for="k in vaultKeeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { profilesService } from "../services/ProfilesService"
import { useRoute } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { onMounted } from "@vue/runtime-core"
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getVaultKeeps(route.params.id)
      } catch (error) {
        logger.error(error)
        Pop.toast("Something went wrong!", 'error')
      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.vaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>