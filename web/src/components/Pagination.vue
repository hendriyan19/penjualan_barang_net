<template>
    <nav class="flex flex-col md:flex-row justify-between items-start md:items-center space-y-3 md:space-y-0 p-4" aria-label="Table navigation">
      <span class="text-sm font-normal text-gray-500 dark:text-gray-400">
        Showing
        <span class="font-semibold text-gray-900 dark:text-white">{{ rangeStart }}-{{ rangeEnd }}</span>
        of
        <span class="font-semibold text-gray-900 dark:text-white">{{ totalItems }}</span>
      </span>
      <ul class="inline-flex items-stretch -space-x-px">
        <li>
          <a href="#" @click.prevent="prevPage" class="flex items-center justify-center h-full py-1.5 px-3 ml-0 text-gray-500 bg-white rounded-l-lg border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">
            <span class="sr-only">Previous</span>
            <svg class="w-5 h-5" aria-hidden="true" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12.707 5.293a1 1 0 010 1.414L9.414 10l3.293 3.293a1 1 0 01-1.414 1.414l-4-4a1 1 0 010-1.414l4-4a1 1 0 011.414 0z"></path>
            </svg>
          </a>
        </li>
        <li v-for="pageNumber in pageNumbers" :key="pageNumber">
          <a href="#" @click.prevent="goToPage(pageNumber)"
            :class="{ 'text-primary-600 bg-primary-50': page === pageNumber, 'text-gray-500 bg-white': page !== pageNumber }"
            class="flex items-center justify-center text-sm py-2 px-3 leading-tight border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">
            {{ pageNumber }}
          </a>
        </li>
        <li>
          <a href="#" @click.prevent="nextPage" class="flex items-center justify-center h-full py-1.5 px-3 ml-0 text-gray-500 bg-white rounded-l-lg border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">
            <span class="sr-only">Next</span>
            <svg class="w-5 h-5" aria-hidden="true" fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
              <path fill-rule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clip-rule="evenodd" />
            </svg>
          </a>
        </li>
      </ul>
    </nav>
  </template>
  
  <script>
  export default {
    props: {
      page: Number,
      totalItems: Number,
      rangeStart: Number,
      rangeEnd: Number,
      pageNumbers: Array,
    },
    methods: {
      prevPage() {
        this.$emit('prev-page');
      },
      nextPage() {
        this.$emit('next-page');
      },
      goToPage(pageNumber) {
        this.$emit('go-to-page', pageNumber);
      },
    },
  };
  </script>