<template>
  <div
    class="modal fade modal-dialog-bg"
    id="detailsModal"
    aria-hidden="true"
    aria-labelledby="detailsModal"
    tabindex="-1"
  >
    <div class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content">
        <div class="modal-body p-0">
          <div class="row">
            <div class="col-md-6">
              <img
                class="details-img-container"
                :src="activeKeep.img"
                alt="RecipeImage"
              />
            </div>
            <div class="col-md-6 container-fluid">
              <div class="row">
                <div class="col-2 ms-auto text-end m-2">
                  <button
                    type="button"
                    class="btn-close"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
              </div>
              <div class="row">
                <div class="col d-inline-flex">
                  <h1 class="text-primary det-font fs-1 px-4">
                    {{ activeKeep.name }}
                  </h1>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, reactive } from "@vue/reactivity"
import { AppState } from "../AppState"
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
export default {
  setup() {
    const state = reactive({
      editable: {},
    });
    return {
      state,
      activeKeep: computed(() => AppState.activeKeep),

    }
  }
}
</script>


<style lang="scss" scoped>
.details-img-container {
  max-height: 500px;
  max-width: 600px;
  object-fit: cover;
}
.pill-bg {
  color: white;
  background: rgba(49, 49, 49, 0.658);
  padding-inline: 20px;
  border-radius: 15px;
}
#addStep {
  background: none;
  outline: none;
  box-shadow: none;
  border: none;
}
.modal-dialog-bg {
  backdrop-filter: blur(10px);
}
</style>