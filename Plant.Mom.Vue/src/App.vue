<template>
  <v-app>
    <v-app-bar
    app
    flat
    color="white"
    >
      <v-toolbar-items>
        <v-btn plain color="white">
          <v-icon color="deep-purple">mdi-menu</v-icon>
        </v-btn>
      </v-toolbar-items>
      <v-toolbar-title class="deep-purple px-1">
        <span class="font-logo white--text">Plant Mom</span>
        <v-icon color="white" class="pl-2" size="22pt">mdi-sprout</v-icon>
      </v-toolbar-title>
      
    </v-app-bar>
    <v-main class="grey lighten-4">
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<style>
.font-logo {
  font-family: 'Lora', serif !important;
  font-weight: 700 !important;
  letter-spacing: -1px;
}
</style>