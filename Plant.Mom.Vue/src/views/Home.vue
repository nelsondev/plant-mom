<template>
  <v-container>
    <v-card flat>
      <v-card-title>Welcome back, Nelson</v-card-title>
      <v-divider></v-divider>
      <v-card-text>
        <v-sheet class="v-sheet--offset mx-auto">

        <v-sparkline
        :line-width="3"
        auto-draw
        :smooth="16"
        :gradient="['#4CAF50', '#673AB7']"
        :value="[
          60,
          70,
          71,
          62,
          73
        ]"
        :labels="[
          '12am',
          '3am',
          '6am',
          '9am',
          '12pm',
          '3pm',
          '6pm',
          '9pm',
        ]"
        ></v-sparkline>
        </v-sheet>
      </v-card-text>
    </v-card>
    <div class="my-3"></div>
    <v-card flat>
      <v-card-title>
        <span>Work</span>
        <v-spacer></v-spacer>
        <v-icon color="success" small>mdi-circle</v-icon>
      </v-card-title>
      <v-divider></v-divider>
      <v-card-text>
      </v-card-text>
    </v-card>
  </v-container>
</template>