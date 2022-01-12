<template>
  <div class="AccountOffcanvas">
    <div
      class="offcanvas offcanvas-end"
      tabindex="-1"
      id="offcanvasRight"
      aria-labelledby="offcanvasRightLabel"
    >
      <div class="offcanvas-header">
        <h5 id="offcanvasRightLabel">Manage Your Account</h5>
        <button
          type="button"
          class="btn-close text-reset"
          data-bs-dismiss="offcanvas"
          aria-label="Close"
        ></button>
      </div>
      <div class="offcanvas-body">
        <div class="row text-center">
          <div class="col">
            <img
              :src="account.picture"
              alt="user photo"
              class="rounded-circle profile-img"
            />
            <h3>{{ account.name }}</h3>
          </div>
        </div>
        <form @submit.prevent="edit">
          <div class="row">
            <h5 class="mt-4">Edit Your Account Info</h5>
            <div class="mt-4">
              <label for="name" class="form-label">Account Name</label>
              <input
                type="text"
                class="form-control"
                name="name"
                id="name"
                aria-describedby="name"
                placeholder="Name..."
                v-model="state.editable.name"
              />
            </div>
            <div class="mt-4">
              <label for="picture" class="form-label">Account Picture</label>
              <input
                type="url"
                class="form-control"
                name="picture"
                id="picture"
                aria-describedby="picture"
                placeholder="picture..."
                v-model="state.editable.picture"
              />
            </div>
          </div>
          <div class="modal-footer">
            <button
              type="button"
              class="btn btn-secondary"
              data-bs-dismiss="offcanvas"
            >
              Close
            </button>
            <button type="submit" class="btn btn-primary">Save</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, reactive, ref } from '@vue/reactivity'
import { logger } from '../utils/Logger'
import { accountService } from '../services/AccountService'
import Pop from '../utils/Pop'
import { watchEffect } from '@vue/runtime-core'
import { AppState } from "../AppState"
import { AuthService } from "../services/AuthService"
export default {
  setup() {
    const state = reactive({
      editable: {},
    });
    return {
      state,
      account: computed(() => AppState.account),
      async edit() {
        try {
          await accountService.edit(state.editable)
        } catch (error) {
          logger.error(error)
          Pop.toast('Failed to Edit', 'error')
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.profile-img {
  height: 75px;
  width: 75px;
  object-fit: cover;
}
</style>