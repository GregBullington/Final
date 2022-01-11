<template>
  <div
    class="modal fade modal-dialog-bg"
    id="createKeep"
    aria-hidden="true"
    aria-labelledby="KeepModal"
    tabindex="-1"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header bg-white">
          <h3 class="modal-title" id="exampleModalToggleLabel">New Keep</h3>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <form @submit.prevent="createKeep()">
          <div class="modal-body">
            <div class="row">
              <div class="col">
                <label for="title" class="form-label">Title</label>
                <input
                  type="text"
                  aria-label="Title"
                  placeholder="Title.."
                  class="form-control"
                  v-model="state.editable.name"
                  required
                />
              </div>
            </div>
            <div class="row">
              <div class="col">
                <label for="imgUrl" class="form-label">Img Url</label>
                <input
                  type="text"
                  aria-label="ImgUrl"
                  placeholder="Image Url.."
                  class="form-control"
                  v-model="state.editable.img"
                  required
                />
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col mx-3 mb-4">
              <label for="description" class="form-label">Description</label>
              <textarea
                style="resize: none"
                type="text"
                aria-label="Description.."
                placeholder="Description.."
                class="form-control"
                v-model="state.editable.description"
                required
              />
            </div>
          </div>

          <div class="modal-footer">
            <button
              type="submit"
              class="btn btn-primary"
              data-bs-dismiss="modal"
            >
              Create
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { reactive } from "@vue/reactivity";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService";
export default {
  setup() {
    const state = reactive({
      editable: {},
    });
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.editable)
          state.editable = {}
        } catch (error) {
          logger.error(error)
          Pop.toast("Something went wrong creating keep!", 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.modal-dialog-bg {
  backdrop-filter: blur(10px);
}
</style>