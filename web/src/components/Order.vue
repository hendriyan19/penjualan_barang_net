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
                  <button type="button" @click="openModalAdd" data-modal-target="createProductModal" data-modal-toggle="createProductModal" class="w-full md:w-auto flex items-center justify-center py-2 px-4 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                      <svg class="h-3.5 w-3.5 mr-2" fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                          <path clip-rule="evenodd" fill-rule="evenodd" d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z" />
                      </svg>
                      Add Order
                  </button>
              </div>
          </div>
          <div class="overflow-x-auto">
              <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                  <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                      <tr>
                          <th scope="col" class="px-4 py-4">No</th>
                          <th scope="col" class="px-4 py-3">Item Name</th>
                          <th scope="col" class="px-4 py-3">Tanggal Order</th>
                          <th scope="col" class="px-4 py-3">Customer Email</th>
                          <th scope="col" class="px-4 py-3">Customer Phone</th>
                          <th scope="col" class="px-4 py-3">Actions</th>
                      </tr>
                  </thead>
                  <tbody>
                      <tr v-for="(order,index) in orders" :key="order.id" class="border-b dark:border-gray-700">
                          <th scope="row" class="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">{{ index+1 }}</th>
                          <td class="px-4 py-3">{{ order.item_Name }}</td>
                          <td class="px-4 py-3">{{ formatDate(order.order_Date) }}</td>
                          <td class="px-4 py-3">{{ order.customer_Email }}</td>
                          <td class="px-4 py-3">{{ order.customer_Phone }}</td>
                          <td class="px-4 py-3 flex items-center justify-end">
                            
                            <button type="button" @click="openModalEdit(order.id)" class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                            Edit
                            </button>
                              <button type="button" class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">              
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
                  <span class="font-semibold text-gray-900 dark:text-white">1-10</span>
                  of
                  <span class="font-semibold text-gray-900 dark:text-white">1000</span>
              </span>
              <ul class="inline-flex items-stretch -space-x-px">
                  <li>
                      <a href="#" class="flex items-center justify-center h-full py-1.5 px-3 ml-0 text-gray-500 bg-white rounded-l-lg border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">
                          <span class="sr-only">Previous</span>
                          <svg class="w-5 h-5" aria-hidden="true" fill="currentColor" viewbox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                              <path fill-rule="evenodd" d="M12.707 5.293a1 1 0 010 1.414L9.414 10l3.293 3.293a1 1 0 01-1.414 1.414l-4-4a1 1 0 010-1.414l4-4a1 1 0 011.414 0z" clip-rule="evenodd" />
                          </svg>
                      </a>
                  </li>
                  <li>
                      <a href="#" class="flex items-center justify-center text-sm py-2 px-3 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">1</a>
                  </li>
                  <li>
                      <a href="#" class="flex items-center justify-center text-sm py-2 px-3 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">2</a>
                  </li>
                  <li>
                      <a href="#" aria-current="page" class="flex items-center justify-center text-sm z-10 py-2 px-3 leading-tight text-primary-600 bg-primary-50 border border-primary-300 hover:bg-primary-100 hover:text-primary-700 dark:border-gray-700 dark:bg-gray-700 dark:text-white">3</a>
                  </li>
                  <li>
                      <a href="#" class="flex items-center justify-center text-sm py-2 px-3 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">...</a>
                  </li>
                  <li>
                      <a href="#" class="flex items-center justify-center text-sm py-2 px-3 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">100</a>
                  </li>
                  <li>
                      <a href="#" class="flex items-center justify-center h-full py-1.5 px-3 leading-tight text-gray-500 bg-white rounded-r-lg border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white">
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
  <div class="flex flex-wrap -mx-3 mb-6">
    
    <div class="w-full px-3">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
        Item Name
      </label>
      <!-- <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="item_name_edit" type="text"> -->
      <select class="p-2 border rounded-lg" v-model="selectedItemId">
        <option v-for="item in items" :key="item.id" :value="item.id">
          {{ item.item_Name }}
        </option>
      </select>
    </div>
  </div>
  <div class="flex flex-wrap -mx-3 mb-6">
    
    <div class="w-full px-3">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
        Order Date
      </label>
      <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="order_date_edit" type="text" disabled>
    </div>
  </div>
  <div class="flex flex-wrap -mx-3 mb-2">
    
    <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-zip">
        Customer Email
      </label>
      <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="customer_email_edit" type="text" disabled>
    </div>
    <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-zip">
        Customer Phone
      </label>
      <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="customer_phone_edit" type="text" disabled>
    </div>
  </div>
  <button @click="updateOrder" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
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
  <div class="flex flex-wrap -mx-3 mb-6">
    
    <div class="w-full px-3">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
        Item Name
      </label>
      <!-- <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="item_name_edit" type="text"> -->
      <select class="p-2 border rounded-lg" v-model="selectedItemId">
        <option v-for="item in items" :key="item.id" :value="item.id">
          {{ item.item_Name }}
        </option>
      </select>
    </div>
  </div>
  <div class="flex flex-wrap -mx-3 mb-6">
    
    <div class="w-full px-3">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
        Order Date
      </label>
      <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="order_date_add" type="date" placeholder="YYYY-MM-DD">
    </div>
  </div>
  <div class="flex flex-wrap -mx-3 mb-2">
    
    <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
        Customer Email
      </label>
      <!-- <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="item_name_edit" type="text"> -->
      <select class="p-2 border rounded-lg" v-model="selectedCustomerId">
        <option v-for="item in customers" :key="item.id" :value="item.id">
          {{ item.customer_Email }}
        </option>
      </select>
    </div>
    <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
      <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
        Customer Phone
      </label>
      <!-- <input class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="item_name_edit" type="text"> -->
      <select class="p-2 border rounded-lg" v-model="selectedCustomerPhone">
        <option v-for="item in customers" :key="item.id" :value="item.id">
          {{ item.customer_Phone }}
        </option>
      </select>
    </div>
  </div>
  <button @click="addOrder" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
  Addd
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
  
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      orders: [],
      items:[],
      customers:[],
      dates:[],
      selectedItemId:null,
      orderId:null,
      selectedCustomerId:null,
      selectedCustomerPhone:null,
    };
  },
  mounted() {
    this.fetchOrders();
    this.fetchItems();
    this.fetchDates();
    this.fetchCustomers();
  },
  watch:{
    selectedCustomerId(newVal){
      
      this.selectedCustomerPhone=newVal;
      
    },
    selectedCustomerPhone(newVal){
      
      this.selectedCustomerId=newVal;
      
    }
  },
  methods: {
    fetchOrders() {
      // Mengambil data dari API menggunakan Axios
      axios
        .get("https://localhost:5001/API/orders/getallorder")
        .then((response) => {
          this.orders = response.data.data;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    fetchItems(){
      axios
        .get("https://localhost:5001/API/orders/getallitem")
        .then((response) => {
          this.items= response.data;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    fetchDates(){
      axios
        .get("https://localhost:5001/API/orders/getallDate")
        .then((response) => {
          this.dates= response.data;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    fetchCustomers(){
      axios
        .get("https://localhost:5001/API/orders/getallcustomer")
        .then((response) => {
          this.customers= response.data;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    formatDate(dateString) {
      // Memformat tanggal ke dalam "YYYY-MM-DD"
      const date = new Date(dateString);
      return date.toISOString().split('T')[0];
    },
    openModalEdit(itemId) {
        //this.editItemId = itemId;
    axios
        .get(`https://localhost:5001/API/orders/getorderbyorderid/${itemId}`)
        .then((response) => {
            const orderData = response.data.data[0];
            const selectedItem = this.items.find(item => item.item_Name === orderData.item_Name);
                if (selectedItem) {
                    this.selectedItemId = selectedItem.id;
                }
            this.orderId = orderData.id
            const date = this.formatDate(orderData.order_Date);
            // document.getElementById('item_name_edit').value = orderData.item_Name;
            document.getElementById('order_date_edit').value = date;
            document.getElementById('customer_email_edit').value = orderData.customer_Email;
            document.getElementById('customer_phone_edit').value = orderData.customer_Phone;
        })
        .catch((error) => {
            console.error("Terjadi kesalahan:", error);
        });
    document.getElementById('editModal').classList.remove('hidden');
    },
    updateOrder() {
        const itemId = this.selectedItemId; 
        const requestBody = {
        item_Id:itemId,
        id:this.orderId
        };
        axios
            .put("https://localhost:5001/API/orders/UpdateOrder", requestBody)
            .then((response) => {
            
            console.log("Item updated successfully");
            this.closeModalEdit();
            this.fetchOrders();
            this.fetchItems();
            })
            .catch((error) => {
            console.error("Failed to update item:", error);
            });
    },
    addOrder() {
    const orderDate = document.getElementById('order_date_add').value;

    const requestBody = {
      order_Date: orderDate,
      customer_Id: this.selectedCustomerId,
      item_Id: this.selectedItemId
    };
    axios
      .post("https://localhost:5001/API/orders/AddOrder", requestBody)
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
  },
};
</script>

<style scoped>
</style>
