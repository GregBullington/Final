<template>
  <div class="container-fluid p-5">
    <div class="row justify-content-around">
      <div
        v-for="k in allKeeps"
        :key="k.id"
        class="col-md-3"
        @click="setActive(k)"
        data-bs-toggle="modal"
        data-bs-target="#detailsModal"
      >
        <Keep :keep="k" />
      </div>
    </div>
    <button
      v-if="user.isAuthenticated"
      class="mdi mdi-plus floating-btn-right"
      data-bs-toggle="modal"
      data-bs-target="#createKeep"
    ></button>
  </div>
  <DetailsModal />
  <CreateKeepModal />
</template>

<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted } from "@vue/runtime-core"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
export default {
  name: 'Home',
  setup() {
    const search = ref('')
    onMounted(async () => {
      try {
        await keepsService.getAllKeeps('api/keeps')
      } catch (error) {
        logger.log(error)
        Pop.toast("Something went wrong!", 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      allKeeps: computed(() => AppState.allKeeps),
      search,

      async searchKeeps() {
        try {
          logger.log("searching")
          await keepsService.getAllKeeps('api/recipes/search?q=' + search.value)
          search.value = ''
        } catch (error) {
          logger.error(error)
        }
      },
      async setActive(keep) {
        try {
          AppState.activeKeep = keep
        } catch (error) {
          logger.error(error)
          Pop.toast("Something went wrong setting active keep!", 'error')
        }
      },
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
