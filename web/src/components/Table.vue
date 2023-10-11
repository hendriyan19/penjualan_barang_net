<template>
    <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
      <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
        <tr>
          <th v-for="(header, index) in tableHeaders" :key="index" scope="col" class="px-4 py-4">{{ header }}</th>
        </tr>
      </thead>
        <slot></slot>
    </table>
  </template>
  
  <script>
  export default {
    props: {
      tableHeaders: Array, // Array of column headers
    }
  };
  </script>