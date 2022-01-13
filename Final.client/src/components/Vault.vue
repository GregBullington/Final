<template>
  <div class="row" @click="setActiveVault" title="Go to Vault">
    <div class="col hero-container card elevation-4 selectable p-0 mx-3 mb-4">
      <i
        class="mdi mdi-lock top-right text-danger mdi-48px text-shadow"
        v-if="vault.isPrivate"
      ></i>
      <img class="img-container" :src="vault.img" alt="vault Image" />
      <div class="card desc-bg bottom-left det-font">
        <h5 class="mt-2">{{ vault.name }}</h5>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { router } from "../router"
export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup(props) {
    return {

      setActiveVault() {
        try {
          AppState.activeVault = props.vault
          router.push({
            name: "Vault",
            params: { id: props.vault.id }
          })
        } catch (error) {
          logger.error(error)
          Pop.toast("Something went wrong setting active vault!", 'error')
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
</style>