<template>
  <div class="component p-5">
    <div class="row">
      <div class="col-md-6">
        <h1>{{ vault.name }}</h1>
        <h6>Keeps: {{ vaultKeeps.length }}</h6>
      </div>
      <div class="col-md-6 d-flex justify-content-end align-items-center">
        <button
          class="btn btn-outline-danger mb-3"
          @click="deleteVault(vault.id)"
          title="Delete Vault"
        >
          Delete Vault
        </button>
      </div>
    </div>
    <div class="row">
      <div class="col-md-2" v-for="k in vaultKeeps" :key="k.vaultKeepId">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>

// REVIEW private vaults dont quite do what they are supposed to.


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRoute, useRouter } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { onMounted } from "@vue/runtime-core"
import { vaultsService } from "../services/VaultsService"
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await vaultsService.getVaultKeeps(route.params.id)
      } catch (error) {
        router.push({ name: 'Home' })
        logger.error(error)
        Pop.toast("Something went wrong!", 'error')
      }
    })
    return {
      vault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.vaultKeeps),

      async deleteVault(id) {
        try {
          if (await Pop.confirm("Are you sure?")) {
            await vaultsService.deleteVault(id)
            router.push({
              name: "Profile",
              params: { id: AppState.myProfile.id }
            })
            Pop.toast("Completed!", 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.toast("Something went wrong deleting vault!", 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>