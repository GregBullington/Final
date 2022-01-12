<template>
  <div class="about p-5">
    <div class="row">
      <div class="col-md-3">
        <img class="rounded mb-5" :src="account.picture" alt="" />
      </div>
      <div class="col-9">
        <h1 class="mb-4">{{ account.name }}</h1>
        <h3>Vaults:</h3>
        <h3 class="mb-4">Keeps:</h3>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <h2>
          Vaults<button
            class="btn mdi mdi-plus mdi-24px text-secondary"
          ></button>
        </h2>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <h2>
          Keeps<button
            class="btn mdi mdi-plus mdi-24px text-secondary"
          ></button>
        </h2>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account),
      activeProfile: computed(() => AppState.activeProfile)
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
