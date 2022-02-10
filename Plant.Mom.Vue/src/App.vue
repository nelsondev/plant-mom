<template>
  <v-app>
    <Menu />
    <v-main class="grey lighten-4">
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({
  components: {
    Menu: () => import("@/components/Menu.vue")
  }
})
</script>

<style>
.font-logo {
  font-family: 'Lora', serif !important;
  font-weight: 700 !important;
  letter-spacing: -1px;
}
</style>