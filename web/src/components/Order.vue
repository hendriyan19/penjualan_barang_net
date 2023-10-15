<template>
  <section class="bg-gray-50 dark:bg-gray-900 p-3 sm:p-5 antialiased">
  <div class="mx-auto max-w-screen-xl px-4 lg:px-12">
    <!-- Start coding here -->
    <div class="bg-white dark:bg-gray-800 relative shadow-md sm:rounded-lg overflow-hidden">
    <div class="overflow-x-auto">
      <SearchComponents
    :searchItems="searchOrders"
    @performSearch="performSearch"
    @openModalAdd="openModalAdd"
    @openModalDeleted="openModalDeleted"
  ></SearchComponents>
  <ReuseableTable :tableHeaders="['No', 'Item Name', 'Tanggal Order', 'Customer Email','Customer Phone']">
    <tbody>
              <tr v-for="(order, index) in orders" :key="order.id" class="border-b dark:border-gray-700">
                <th scope="row" class="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">{{ index + 1
                }}</th>
                <td class="px-4 py-3">{{ order.item_Name }}</td>
                <td class="px-4 py-3">{{ formatDate(order.order_Date) }}</td>
                <td class="px-4 py-3">{{ order.customer_Email }}</td>
                <td class="px-4 py-3">{{ order.customer_Phone }}</td>
                <td class="px-4 py-3 flex items-center justify-end">

                  <button type="button" @click="openModalEdit(order.id)"
                    class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                    Edit
                  </button>
                  <button @click.prevent="softDeleteItem(order.id)" type="button"
                    class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                    Delete
                  </button>
                </td>
              </tr>

            </tbody>
        </ReuseableTable>
      </div>
      <Pagination
    :page="page"
    :totalItems="totalItems"
    :rangeStart="getRangeStart()"
    :rangeEnd="getRangeEnd()"
    :pageNumbers="pageNumbers"
    @prev-page="prevPage"
    @next-page="nextPage"
    @go-to-page="goToPage"
  />

    </div>
  </div>
</section>


  <!-- Edit Modal -->
  <modal v-if="isModalEditOpen" @close="closeModalEdit">
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
              <input
                class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                v-model="input_order_date" type="text" disabled>
            </div>
          </div>
          <div class="flex flex-wrap -mx-3 mb-2">

            <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
              <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-zip">
                Customer Email
              </label>
              <input
                class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                v-model="input_customer_email" type="text" disabled>
            </div>
            <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
              <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-zip">
                Customer Phone
              </label>
              <input
                class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                v-model="input_customer_phone" type="text" disabled>
            </div>
          </div>
          <button @click.prevent="updateOrder"
            class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
            Update
          </button>
        </form>
  </modal>


  <!-- Add Modal -->
  <modal v-if="isModalAddOpen" @close="closeModalAdd">
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
              <input
                class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                v-model="input_order_date" type="date" placeholder="YYYY-MM-DD">
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
          <button @click.prevent="addOrder" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
            Addd
          </button>
        </form>
  </modal>


  <!-- Deleted Modal -->
  <modal v-if="isModalDeletedOpen" @close="closeModalDeleted">
    <table class="min-w-full text-left text-sm font-light">
                  <thead class="border-b font-medium dark:border-neutral-500">
                    <tr>
                      <th scope="col" class="px-6 py-4">No</th>
                      <th scope="col" class="px-6 py-4">Item Name</th>
                      <th scope="col" class="px-6 py-4">Order Date</th>
                      <th scope="col" class="px-6 py-4">Customer Email</th>
                      <th scope="col" class="px-6 py-4">Customer Phone</th>
                      <th scope="col" class="px-6 py-4">Action</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="(item, index) in deletedOrders" :key="item.id" class="border-b dark:border-neutral-500">
                      <td class="whitespace-nowrap px-6 py-4 font-medium">{{ index + 1 }}</td>
                      <td class="whitespace-nowrap px-6 py-4">{{ item.item_Name }}</td>
                      <td class="whitespace-nowrap px-6 py-4">{{ item.order_Date }}</td>
                      <td class="whitespace-nowrap px-6 py-4">{{ item.customer_Email }}</td>
                      <td class="whitespace-nowrap px-6 py-4">{{ item.customer_Phone }}</td>
                      <td class="whitespace-nowrap px-6 py-4">


                        <button @click.prevent="restoreOrder(item.id)"
                          class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                          Restore
                        </button>


                      </td>
                    </tr>
                  </tbody>
                </table>
  </modal>
</template>

<script>
import axios from "axios";
import Swal from 'sweetalert2'
import Modal from './Modal.vue';
import ReuseableTable from './Table.vue';
import Pagination from './Pagination.vue';
import SearchComponents from './SearchComponents.vue';
export default {
  components: {
    Modal,
    ReuseableTable,
    Pagination,
    SearchComponents,
  },
  data() {
    return {
      orders: [],
      items: [],
      customers: [],
      dates: [],
      selectedItemId: null,
      orderId: null,
      selectedCustomerId: null,
      selectedCustomerPhone: null,
      deletedOrders: [],
      page: 1,
      totalPage: 1,
      totalOrders: 1,
      isModalEditOpen: false,
      isModalAddOpen: false,
      isModalDeletedOpen: false,
      input_order_date: '',//order_date_edit
      input_customer_email: '',//customer_email_edit
      input_customer_phone: '',//customer_phone_edit
      searchOrders: "",
    };
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
  mounted() {
    this.fetchOrders();
    this.fetchItems();
    this.fetchDates();
    this.fetchCustomers();
    this.DeletedItems();
  },
  watch: {
    selectedCustomerId(newVal) {

      this.selectedCustomerPhone = newVal;

    },
    selectedCustomerPhone(newVal) {

      this.selectedCustomerId = newVal;

    }
  },
  methods: {
    fetchOrders() {
      // Mengambil data dari API menggunakan Axios
      axios
        .get(`https://localhost:5001/API/orders/getallorder?page=${this.page}&pageSize=10`)
        .then((response) => {
          this.orders = response.data.data;
          this.totalPage = Math.ceil(response.data.meta.pagination.count / 10);
          this.totalOrders = response.data.meta.pagination.count;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    fetchItems() {
      axios
        .get("https://localhost:5001/API/orders/getallitem")
        .then((response) => {
          this.items = response.data;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    fetchDates() {
      axios
        .get("https://localhost:5001/API/orders/getallDate")
        .then((response) => {
          this.dates = response.data;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    fetchCustomers() {
      axios
        .get("https://localhost:5001/API/orders/getallcustomer")
        .then((response) => {
          this.customers = response.data;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    formatDate(dateString) {
      const dateParts = dateString.split('-');
      if (dateParts.length === 3) {
        const year = parseInt(dateParts[0]);
        const month = parseInt(dateParts[1]) - 1; // Bulan dimulai dari 0 (Januari) - 11 (Desember)
        const day = parseInt(dateParts[2]);

        const date = new Date(Date.UTC(year, month, day));

        const formattedYear = date.getUTCFullYear();
        const formattedMonth = String(date.getUTCMonth() + 1).padStart(2, '0');
        const formattedDay = String(date.getUTCDate()).padStart(2, '0');

        return `${formattedYear}-${formattedMonth}-${formattedDay}`;
      } else {
        return dateString; // Kembalikan string aslinya jika tidak sesuai format
      }
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
          this.input_order_date = date;
          this.input_customer_email = orderData.customer_Email;
          this.input_customer_phone = orderData.customer_Phone;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
      this.isModalEditOpen = true;
    },
    updateOrder() {
      const itemId = this.selectedItemId;

      // Tambahkan konfirmasi Swal di sini
      Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin mengupdate order ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal',
      }).then((result) => {
        if (result.isConfirmed) {
          const requestBody = {
            item_Id: itemId,
            id: this.orderId
          };

          axios
            .put("https://localhost:5001/API/orders/UpdateOrder", requestBody)
            .then((response) => {
              this.closeModalEdit();
              this.fetchOrders();
              this.fetchItems();

              // Tampilkan pesan Swal setelah berhasil mengupdate order
              Swal.fire('Sukses', 'Order berhasil diupdate', 'success');
            })
            .catch((error) => {
              console.error("Gagal mengupdate order:", error);
            });
        }
      });
    },
    addOrder() {
      const orderDate = this.input_order_date;


      // Tambahkan konfirmasi Swal di sini
      Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin menambahkan order ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal',
      }).then((result) => {
        if (result.isConfirmed) {
          const requestBody = {
            order_Date: orderDate,
            customer_Id: this.selectedCustomerId,
            item_Id: this.selectedItemId
          };

          axios
            .post("https://localhost:5001/API/orders/AddOrder", requestBody)
            .then((response) => {
              this.closeModalAdd();
              this.fetchOrders();
              this.fetchItems();
              Swal.fire('Sukses', 'Order berhasil ditambahkan', 'success');
            })
            .catch((error) => {
              console.error("Gagal menambahkan order:", error);
            });
        }
      });
    },
    DeletedItems() {
      // Mengambil data dari API menggunakan Axios
      axios
        .get("https://localhost:5001/API/orders/getalldeleted")
        .then((response) => {
          this.deletedOrders = response.data;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
    },
    softDeleteItem(orderId) {
      const order_Id = orderId;

      // Tambahkan konfirmasi Swal di sini
      Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin menghapus order ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal',
      }).then((result) => {
        if (result.isConfirmed) {
          const requestBody = {
            id: order_Id
          };

          axios
            .put("https://localhost:5001/API/orders/DeleteItem", requestBody)
            .then((response) => {
              this.fetchOrders(); // Mengambil data lagi setelah order dihapus
              this.DeletedItems();
              Swal.fire('Sukses', 'Order berhasil dihapus', 'success');
            })
            .catch((error) => {
              console.error("Gagal menghapus order:", error);
            });
        }
      });
    },
    restoreOrder(orderId) {
      const order_Id = orderId;

      // Tambahkan konfirmasi Swal di sini
      Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin mengembalikan order ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal',
      }).then((result) => {
        if (result.isConfirmed) {
          const requestBody = {
            id: order_Id
          };

          axios
            .put("https://localhost:5001/API/orders/RestoreItem", requestBody)
            .then((response) => {
              this.fetchOrders(); // Mengambil data lagi setelah order direstore
              this.DeletedItems();

              // Tampilkan pesan Swal setelah berhasil merestore order
              Swal.fire('Sukses', 'Order berhasil direstore', 'success');
            })
            .catch((error) => {
              console.error("Gagal merestore order:", error);
            });
        }
      });
    },

    closeModalEdit() {
      this.isModalEditOpen = false;
    },
    openModalAdd() {
      this.isModalAddOpen = true;
    },
    closeModalAdd() {
      this.isModalAddOpen = false;
    },
    closeModalDeleted() {
      this.isModalDeletedOpen = false;
    },
    openModalDeleted() {
      this.isModalDeletedOpen = true;
    },
    nextPage() {
      this.page++;
      this.fetchOrders();
    },
    prevPage() {
      if (this.page > 1) {
        this.page--;
        this.fetchOrders();
      }
    },
    goToPage(pageNumber) {
      if (pageNumber >= 1 && pageNumber <= this.totalPage) {
        this.page = pageNumber;
        this.fetchOrders();
      }
    },
    getRangeStart() {
      return (this.page - 1) * 10 + 1;
    },
    getRangeEnd() {
      const end = this.page * 10;
      return end > this.totalOrders ? this.totalOrders : end;
    },
    performSearch() {
    // Call your search function here using Vue.js data
    axios
      .get(`https://localhost:5001/API/items/search/${this.searchItems}`)
      .then((response) => {
        // Handle the search results here
        this.resultSearch = response.data;
        console.log(this.resultSearch);
      })
      .catch((error) => {
        console.error(error);
      });
  },
  },
};
</script>

<style scoped></style>
