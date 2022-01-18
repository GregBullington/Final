<template>
  <Modal id="detailsModal" size="modal-xl">
    <template #modal-body>
      <div class="modal-body container-fluid p-2" v-if="keep.id">
        <button
          type="button"
          class="btn-close top-right"
          data-bs-dismiss="modal"
          aria-label="Close"
        ></button>
        <div class="row">
          <div class="col-lg-6">
            <img
              class="d-block w-100 max-height"
              :src="keep.img"
              alt="RecipeImage"
            />
          </div>
          <div class="col-lg-6 d-flex flex-column">
            <div class="row">
              <ul class="list-inline text-center">
                <li
                  class="list-inline-item mdi mdi-eye mdi-24px text-secondary"
                ></li>
                <span class="text-dark me-3">{{ keep.views }}</span>
                <li
                  class="
                    list-inline-item
                    mdi mdi-alpha-k-box-outline mdi-24px
                    text-secondary
                  "
                ></li>
                <span class="text-dark me-3">{{ keep.keeps }}</span>

                <li
                  class="
                    list-inline-item
                    mdi mdi-share-variant mdi-24px
                    text-secondary
                  "
                ></li>
                <span class="text-dark">{{ keep.shares }}</span>
              </ul>
            </div>
            <div class="row">
              <div class="col text-center">
                <h1 class="text-dark det-font fs-1 px-4 mb-4">
                  {{ keep.name }}
                </h1>
              </div>
            </div>
            <div class="row justify-content-center">
              <div class="col-md-10">
                <p>{{ keep.description }}</p>
                <hr />
              </div>
            </div>
            <div class="row mt-auto">
              <div
                class="col-4 align-items-center justify-content-center"
                v-if="$route.name != 'Vault'"
              >
                <div class="dropdown mx-4 my-2" v-if="user.isAuthenticated">
                  <button
                    class="btn btn-secondary dropdown-toggle"
                    type="button"
                    id="dropdownMenuButton1"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                    required
                  >
                    {{ vault }}
                  </button>
                  <ul
                    class="dropdown-menu"
                    aria-labelledby="dropdownMenuButton1"
                  >
                    <li v-for="v in profileVaults" :key="v.id">
                      <div
                        class="dropdown-item selectable"
                        @click="addToVault(keep.id, v.id)"
                        title="Add to Vault"
                      >
                        {{ v.name }}
                      </div>
                    </li>
                  </ul>
                </div>
              </div>
              <div
                class="col-4 align-items-center justify-content-center"
                v-else
              >
                <button
                  class="btn btn-outline-danger p-1"
                  @click="removeFromVault(keep.id)"
                  title="Remove from Vault"
                >
                  Remove
                </button>
              </div>
              <div
                class="
                  col-4
                  d-flex
                  align-items-center
                  justify-content-center
                  p-0
                "
              >
                <button
                  v-if="
                    account.id == keep.creatorId && $route.name == 'Profile'
                  "
                  class="btn mdi mdi-delete-outline text-danger mdi-24px p-0"
                  @click="deleteKeep(keep.id)"
                  title="Delete Keep"
                ></button>
              </div>
              <div
                class="
                  col-4
                  d-flex
                  align-items-center
                  justify-content-center
                  flex-wrap
                  p-0
                "
              >
                <img
                  @click="setActiveProfile(keep.creatorId)"
                  title="Go to Profile"
                  class="prof-img rounded action"
                  :src="keep.creator.picture"
                  alt="img"
                />

                <span id="creator-name" class="mx-2">{{
                  keep.creator.name
                }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, reactive, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { profilesService } from "../services/ProfilesService"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"


export default {
  setup() {
    const vault = ref('Add to Vault')
    const router = useRouter()
    const state = reactive({
      editable: {},
    });
    return {
      vault,
      state,
      profileVaults: computed(() => AppState.myProfileVaults),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      keep: computed(() => AppState.activeKeep),
      async setActiveProfile(id) {
        try {
          Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
          await profilesService.setActiveProfile(id)
          router.push({
            name: "Profile",
            params: { id: AppState.activeProfile.id }
          })
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
          Pop.toast("Something went wrong with active profile!", 'error')
        }
      },

      async removeFromVault(id) {
        try {
          if (await Pop.confirm('Remove from Vault?')) {
            Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
            await vaultKeepsService.removeFromVault(id)
          }
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
          Pop.toast("Something went wrong removing!", 'error')
        }
      },

      async deleteKeep(id) {
        try {
          if (await Pop.confirm('Delete Keep?')) {
            Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
            await keepsService.deleteKeep(id)
          }
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
          Pop.toast("Something went wrong deleting!", 'error')
        }
      },

      async addToVault(keepId, vaultId) {
        try {
          Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
          await vaultKeepsService.addToVault(keepId, vaultId)
        } catch (error) {
          logger.error(error)
          Modal.getOrCreateInstance(document.getElementById("detailsModal")).hide()
          Pop.toast("Something went wrong deleting!", 'error')
        }
      }


    }
  }
}
</script>


<style lang="scss" scoped>
.prof-img {
  height: 25px;
  width: 25px;
}
.max-height {
  max-height: 75vh;
  object-fit: cover;
}

@media screen and (max-width: 450px) {
  #creator-name {
    display: none;
  }
}
</style>