<template>
    <section class="bg-gray-50 dark:bg-gray-900 p-3 sm:p-5 antialiased">
    <div class="mx-auto max-w-screen-xl px-4 lg:px-12">
        <!-- Start coding here -->
        <div class="bg-white dark:bg-gray-800 relative shadow-md sm:rounded-lg overflow-hidden">
            <div class="flex flex-col md:flex-row items-center justify-between space-y-3 md:space-y-0 md:space-x-4 p-4">
                <div class="w-full md:w-1/2">
                    <form class="flex items-center">
                        <label for="simple-search" class="sr-only">Search</label>
                        <div class="relative w-full">
                            <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                                <svg aria-hidden="true" class="w-5 h-5 text-gray-500 dark:text-gray-400" fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z" clip-rule="evenodd" />
                                </svg>
                            </div>
                            <input type="text" id="simple-search" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full pl-10 p-2 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Search" required="">
                        </div>
                    </form>
                </div>
                <div class="w-full md:w-auto flex flex-col md:flex-row space-y-2 md:space-y-0 items-stretch md:items-center justify-end md:space-x-3 flex-shrink-0">
                    <button type="button" @click="openModalAdd" class="w-full md:w-auto flex items-center justify-center py-2 px-4 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                        <svg class="h-3.5 w-3.5 mr-2" fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                            <path clip-rule="evenodd" fill-rule="evenodd" d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z" />
                        </svg>
                        Add product
                    </button>
                    <button type="button" @click="openModalDeleted" class="w-full md:w-auto flex items-center justify-center py-2 px-4 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                        <svg class="h-3.5 w-3.5 mr-2" fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                            <path clip-rule="evenodd" fill-rule="evenodd" d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z" />
                        </svg>
                        Deleted Product
                    </button>
                </div>
            </div>
            <div class="overflow-x-auto">
                <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            <th scope="col" class="px-4 py-4">No</th>
                            <th scope="col" class="px-4 py-3">Item Name</th>
                            <th scope="col" class="px-4 py-3">Item Price</th>
                            <th scope="col" class="px-4 py-3">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item,index) in items" :key="item.id" class="border-b dark:border-gray-700">
                            <th scope="row" class="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">{{ index+1 }}</th>
                            <td class="px-4 py-3">{{ item.item_Name }}</td>
                            <td class="px-4 py-3">{{ formatPrice(item.item_Price) }}</td>
                            <td class="px-4 py-3 flex items-center justify-end">
                              <button type="button" @click="openModalEdit(item.id)" class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">         
                                                Edit
                                </button>
                                <button type="button" @click="softDeleteItem(item.id)" class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">              
                                                Delete
                                </button>
                            </td>
                        </tr>
                        
                    </tbody>
                </table>
            </div>


            <nav class="flex flex-col md:flex-row justify-between items-start md:items-center space-y-3 md:space-y-0 p-4" aria-label="Table navigation">
              <span class="text-sm font-normal text-gray-500 dark:text-gray-400">
              Showing
              <span class="font-semibold text-gray-900 dark:text-white">{{ getRangeStart() }}-{{ getRangeEnd() }}</span>
              of
              <span class="font-semibold text-gray-900 dark:text-white">{{ totalItems }}</span>
            </span>
                <ul class="inline-flex items-stretch -space-x-px">
                    <li>
                      <a
                        href="#"
                        @click.prevent="prevPage"
                        class="flex items-center justify-center h-full py-1.5 px-3 ml-0 text-gray-500 bg-white rounded-l-lg border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white"
                      >
                        <span class="sr-only">Previous</span>
                        <svg
                          class="w-5 h-5"
                          aria-hidden="true"
                          fill="currentColor"
                          viewBox="0 0 20 20"
                          xmlns="http://www.w3.org/2000/svg"
                        >
                          <path
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            stroke-width="2"
                            d="M12.707 5.293a1 1 0 010 1.414L9.414 10l3.293 3.293a1 1 0 01-1.414 1.414l-4-4a1 1 0 010-1.414l4-4a1 1 0 011.414 0z"
                          ></path>
                        </svg>
                      </a>
                    </li>
                    
                    <li v-for="pageNumber in pageNumbers" :key="pageNumber">
                    <a
                      href="#"
                      @click.prevent="goToPage(pageNumber)"
                      :class="{ 'text-primary-600 bg-primary-50': page === pageNumber, 'text-gray-500 bg-white': page !== pageNumber }"
                      class="flex items-center justify-center text-sm py-2 px-3 leading-tight border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white"
                    >
                      {{ pageNumber }}
                    </a>
                  </li>


                    <li>
                      <a
                      href="#"
                      @click.prevent="nextPage"
                      class="flex items-center justify-center h-full py-1.5 px-3 ml-0 text-gray-500 bg-white rounded-l-lg border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white"
                    >
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
</section>



<!-- Edit Modal -->
<div id="editModal" class="fixed inset-0 z-10 flex items-center justify-center overflow-x-hidden overflow-y-auto hidden">
    <div class="modal-overlay absolute w-full h-full bg-gray-900 opacity-50"></div>

    <div class="modal-container bg-white dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 rounded-lg z-50 max-w-lg mx-auto p-4 sm:p-6">
      <!-- Konten Modal di sini -->
      <div class="modal-content">
       
        <form class="w-full max-w-lg">
            <div class="flex flex-wrap -mx-3 mb-2">
    <div class="w-full px-3">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
        Item Name
      </label>
      <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" 
      id="input_item_name" type="text">
    </div>
  </div>
  <div class="flex flex-wrap -mx-3 mb-2">
    <div class="w-full px-3">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
        Price
      </label>
      <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" 
      id="input_item_price" type="text">
    </div>
  </div>
  <button @click="updateItem" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
  Update
</button>
</form>
      </div>

      <!-- Tombol Tutup Modal -->
      <button
        type="button"
        @click="closeModalEdit"
        class="absolute top-0 right-0 m-4 p-2 text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-200 focus:outline-none"
      >
        <svg
          class="w-6 h-6"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M6 18L18 6M6 6l12 12"
          ></path>
        </svg>
      </button>
    </div>
  </div>


  <!-- Add Modal -->
<div id="addModal" class="fixed inset-0 z-10 flex items-center justify-center overflow-x-hidden overflow-y-auto hidden">
    <div class="modal-overlay absolute w-full h-full bg-gray-900 opacity-50"></div>

    <div class="modal-container bg-white dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 rounded-lg z-50 max-w-lg mx-auto p-4 sm:p-6">
      <!-- Konten Modal di sini -->
      <div class="modal-content">
       
        <form class="w-full max-w-lg">
  <div class="flex flex-wrap -mx-3 mb-2">
    <div class="w-full px-3">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
        Item Name
      </label>
      <input id="create_item_name" class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" type="text">
    </div>
  </div>
  <div class="flex flex-wrap -mx-3 mb-2">
    <div class="w-full px-3">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
        Price
      </label>
      <input id="create_item_price" class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white" type="text">
    </div>
  </div>
  <button @click="addItem" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
  Add
</button>
  
</form>
      </div>

      <!-- Tombol Tutup Modal Add-->
      <button
        type="button"
        @click="closeModalAdd"
        class="absolute top-0 right-0 m-4 p-2 text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-200 focus:outline-none"
      >
        <svg
          class="w-6 h-6"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M6 18L18 6M6 6l12 12"
          ></path>
        </svg>
      </button>
    </div>
  </div>


  <!-- Deleted Modal -->
<div id="deletedModal" class="fixed inset-0 z-10 flex items-center justify-center overflow-x-hidden overflow-y-auto hidden">
    <div class="modal-overlay absolute w-full h-full bg-gray-900 opacity-50"></div>

    <div class="modal-container bg-white dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 rounded-lg z-50 max-w-lg mx-auto p-4 sm:p-6">
      <!-- Konten Modal di sini -->
      <div class="modal-content">
       <!-- start modal content -->
       <div class="flex flex-col">
  <div class="overflow-x-auto sm:-mx-6 lg:-mx-8">
    <div class="inline-block min-w-full py-2 sm:px-6 lg:px-8">
      <div class="overflow-hidden">
        <!-- deleteditems -->
        <!-- <tr v-for="(item,index) in items" :key="item.id" -->
        <table class="min-w-full text-left text-sm font-light">
          <thead class="border-b font-medium dark:border-neutral-500">
            <tr >
              <th scope="col" class="px-6 py-4">No</th>
              <th scope="col" class="px-6 py-4">Item Name</th>
              <th scope="col" class="px-6 py-4">Item Price</th>
              <th scope="col" class="px-6 py-4">Action</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item,index) in deleteditems" :key="item.id" class="border-b dark:border-neutral-500">
              <td class="whitespace-nowrap px-6 py-4 font-medium">{{ index+1 }}</td>
              <td class="whitespace-nowrap px-6 py-4">{{ item.item_Name }}</td>
              <td class="whitespace-nowrap px-6 py-4">{{ item.item_Price }}</td>
              <td class="whitespace-nowrap px-6 py-4">

                
                <button @click="restoreItem(item.id)" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
  Restore
</button>
                

              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</div>
       <!-- end modal content -->
        
       
      </div>

      <!-- Tombol Tutup Modal Deleted-->
      <button
        type="button"
        @click="closeModalDeleted"
        class="absolute top-0 right-0 m-4 p-2 text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-200 focus:outline-none"
      >
        <svg
          class="w-6 h-6"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M6 18L18 6M6 6l12 12"
          ></path>
        </svg>
      </button>
    </div>
  </div>

  </template>



  
  <script>
import axios from 'axios';

  export default {
    data() {
      return {
        items: [],
        deleteditems:[],
        editItemId: null,
        page: 1,
        totalPage: 1,
        totalItems:1,
      };
    },
    mounted() {
      this.fetchItems();
      this.DeletedItems();
    },
    computed: {
    pageNumbers() {
        const pages = [];
        for (let i = 1; i <= this.totalPage; i++) {
          pages.push(i);
        }
        return pages;
      },
    },
    methods: {
      fetchItems() {
        // Update the API URL to include the page number
        axios
          .get(`https://localhost:5001/API/items/getallitem?page=${this.page}&pageSize=10`)
          .then((response) => {
            this.items = response.data.data;
            this.totalPage = Math.ceil(response.data.meta.pagination.count/10); 
            this.totalItems = response.data.meta.pagination.count;
          })
          .catch((error) => {
            console.error("Terjadi kesalahan:", error);
          });
      },
      DeletedItems() {
        // Mengambil data dari API menggunakan Axios
        axios
          .get("https://localhost:5001/API/items/getalldeleted")
          .then((response) => {
            this.deleteditems = response.data.data;
          })
          .catch((error) => {
            console.error("Terjadi kesalahan:", error);
          });
      },
      formatPrice(price) {
      // Memformat harga ke dalam "Rp. 0.000.000,00"
      const formattedPrice = new Intl.NumberFormat("id-ID", {
        style: "currency",
        currency: "IDR",
        minimumFractionDigits: 2,
      }).format(price);
      return formattedPrice;
    },
    openModalEdit(itemId) {
        this.editItemId = itemId;
    axios
        .get(`https://localhost:5001/API/items/getitembyid/${itemId}`)
        .then((response) => {
            const itemData = response.data[0];
            document.getElementById('input_item_name').value = itemData.item_Name;
            document.getElementById('input_item_price').value = itemData.item_Price;
        })
        .catch((error) => {
            console.error("Terjadi kesalahan:", error);
        });
    document.getElementById('editModal').classList.remove('hidden');
},
    closeModalDeleted() {
      // Sembunyikan modal dengan mengubah class "block" menjadi "hidden"
      document.getElementById('deletedModal').classList.add('hidden');
    },
    openModalDeleted() {
      document.getElementById('deletedModal').classList.remove('hidden');
    },
    closeModalEdit() {
      // Sembunyikan modal dengan mengubah class "block" menjadi "hidden"
      document.getElementById('editModal').classList.add('hidden');
    },
    openModalAdd() {
      document.getElementById('addModal').classList.remove('hidden');
    },
    closeModalAdd() {
      document.getElementById('addModal').classList.add('hidden');
    },
    updateItem() {
        const itemId = this.editItemId; 
        const itemName = document.getElementById('input_item_name').value;
        const itemPrice = document.getElementById('input_item_price').value;

        
        const requestBody = {
            id: itemId,
            item_Name: itemName,
            item_Price: parseInt(itemPrice),
        };

        
        axios
            .put("https://localhost:5001/API/items/UpdateItem", requestBody)
            .then((response) => {
            
            console.log("Item updated successfully");
            this.closeModalEdit();
            this.fetchItems();
            })
            .catch((error) => {
            console.error("Failed to update item:", error);
            });
    },
    addItem() {
    const itemName = document.getElementById('create_item_name').value;
    const itemPrice = document.getElementById('create_item_price').value;

    // Membuat objek yang akan dikirim sebagai payload
    const requestBody = {
      Item_Name: itemName,
      Item_Price: parseFloat(itemPrice),
    };

    axios
      .post("https://localhost:5001/API/items/AddItem", requestBody)
      .then((response) => {
        // Handle respon sukses
        console.log("Item added successfully");
        this.closeModalAdd();
        this.fetchItems();
      })
      .catch((error) => {
        console.error("Gagal menambahkan item:", error);
      });
  },
  softDeleteItem(itemId) {

    const item_Id = itemId;

    const requestBody = {
      id: item_Id
    };
        axios
            .put("https://localhost:5001/API/items/DeleteItem", requestBody)
            .then((response) => {
                console.log("Item soft deleted successfully");
                
                this.fetchItems(); // Mengambil data lagi setelah item dihapus
                this.DeletedItems();
            })
            .catch((error) => {
              console.log(requestBody);
                console.error("Failed to soft delete item:", error);
            });
    },
    restoreItem(itemId) {

      const item_Id = itemId;

      const requestBody = {
        id: item_Id
      };
          axios
              .put("https://localhost:5001/API/items/RestoreItem", requestBody)
              .then((response) => {
                  console.log("Item soft deleted successfully");
                  
                  this.fetchItems(); // Mengambil data lagi setelah item dihapus
                  this.DeletedItems();
              })
              .catch((error) => {
                console.log(requestBody);
                  console.error("Failed to soft delete item:", error);
              });
      },
      nextPage() {
        this.page++; 
        this.fetchItems(); 
      },
      prevPage() {
      if (this.page > 1) {
        this.page--; 
        this.fetchItems(); 
      }
      },
      goToPage(pageNumber) {
      if (pageNumber >= 1 && pageNumber <= this.totalPage) {
        this.page = pageNumber; 
        this.fetchItems(); 
      }
    },
    getRangeStart() {
    return (this.page - 1) * 10 + 1; 
    },
    getRangeEnd() {
      const end = this.page * 10; 
      return end > this.totalItems ? this.totalItems : end;
    },
  
  
},
  };
  </script>
  
  <style scoped>
  </style>
  