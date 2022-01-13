<template>
  <div
    class="row"
    @click="setActive(keep.id)"
    data-bs-toggle="modal"
    data-bs-target="#detailsModal"
  >
    <div class="col hero-container card elevation-4 selectable p-0 mx-3 mb-4">
      <img class="img-container" :src="keep.img" alt="keep Image" />
      <div class="card desc-bg bottom-left det-font">
        <h5 class="mt-2">{{ keep.name }}</h5>
        <img
          v-if="keep.creator.picture"
          class="rounded-pill card-img-contain bottom-right"
          :src="keep.creator.picture"
          alt="img"
        />
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { keepsService } from "../services/KeepsService"
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      allKeeps: computed(() => AppState.allKeeps),

      async setActive(id) {
        try {
          await keepsService.getKeepById(id)
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
          Pop.toast("Something went wrong setting active keep!", 'error')
        }
      },


    }
  }
}
</script>


<style lang="scss" scoped>
</style>