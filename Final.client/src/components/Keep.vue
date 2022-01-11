<template>
  <div class="row">
    <div
      class="col hero-container card elevation-4 selectable grow p-0 mx-3 mb-4"
    >
      <img class="img-container" :src="keep.img" alt="keep Image" />
      <div class="card desc-bg bottom-left det-font">
        <h5 class="mt-2">{{ keep.name }}</h5>
        <img
          class="rounded-pill card-img-contain bottom-right"
          :src="keep.creator.picture"
          alt=""
        />
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup() {
    return {
      allKeeps: computed(() => AppState.allKeeps)

    }
  }
}
</script>


<style lang="scss" scoped>
</style>