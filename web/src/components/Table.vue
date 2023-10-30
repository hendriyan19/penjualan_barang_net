<template>
  <section class="bg-gray-50 dark:bg-gray-900 p-3 sm:p-5 antialiased">
  <div class="mx-auto max-w-screen-xl px-4 lg:px-12">
      <!-- Start coding here -->
      <div class="bg-white dark:bg-gray-800 relative shadow-md sm:rounded-lg overflow-hidden">
        <div class="overflow-x-auto">
   <div class="flex flex-col md:flex-row items-center justify-between space-y-3 md:space-y-0 md:space-x-4 p-4">
      <div class="w-full md:w-1/2">
        <form class="flex items-center" @input.prevent="performSearch">
          <label for="simple-search" class="sr-only">Search</label>
          <div class="relative w-full">
            <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
              <svg aria-hidden="true" class="w-5 h-5 text-gray-500 dark:text-gray-400" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z" clip-rule="evenodd" />
              </svg>
            </div>
            <input :value="searchText" @input="searchTextChanged" type="text" id="simple-search" 
            class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg 
            focus:ring-primary-500 focus:border-primary-500 block w-full pl-10 p-2
             dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400
              dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" 
              placeholder="Search" required>
          </div>
        </form>
      </div>
      <div class="w-full md:w-auto flex flex-col md:flex-row space-y-2 md:space-y-0 items-stretch md:items-center justify-end md:space-x-3 flex-shrink-0">
        <button type="button" @click="openModalAdd" class="w-full md:w-auto flex items-center justify-center py-2 px-4 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
          <svg class="h-3.5 w-3.5 mr-2" fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
            <path clip-rule="evenodd" fill-rule="evenodd" d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z" />
          </svg>
          Add Item
        </button>
        <button type="button" @click="openModalDeleted" class="w-full md:w-auto flex items-center justify-center py-2 px-4 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
          <svg class="h-3.5 w-3.5 mr-2" fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
            <path clip-rule="evenodd" fill-rule="evenodd" d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z" />
          </svg>
          Deleted Item
        </button>
      </div>
    </div>
    <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
      <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
        <tr>
          <th v-for="(header, index) in tableHeaders" :key="index" scope="col" class="px-4 py-4">{{ header }}</th>
        </tr>
      </thead>
        <slot></slot>
    </table>
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
    </div>
  </div>
</div>
</section>
  </template>
  
  <script setup>
  import { ref, defineProps, defineEmits } from 'vue';
  
  const props = defineProps([
    'tableHeaders', 'page', 'totalItems', 'rangeStart', 'rangeEnd', 'pageNumbers', 'searchItems'
  ]);
  
  const searchText = ref(props.searchItems);
  
  const emit = defineEmits(['prev-page', 'next-page', 'go-to-page', 'performSearch', 'openModalAdd', 'openModalDeleted']);
  
  const prevPage = () => {
    emit('prev-page');
  };
  
  const nextPage = () => {
    emit('next-page');
  };
  
  const goToPage = (pageNumber) => {
    emit('go-to-page', pageNumber);
  };
  
  const performSearch = () => {
    emit('performSearch', searchText.value);
  };
  
  const openModalAdd = () => {
    emit('openModalAdd');
  };
  
  const openModalDeleted = () => {
    emit('openModalDeleted');
  };
  
  const searchTextChanged = (event) => {
    searchText.value = event.target.value;
  };
  </script>
  