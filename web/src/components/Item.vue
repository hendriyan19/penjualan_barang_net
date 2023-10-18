<template>
          <ReuseableTable :tableHeaders="['No', 'Item Name', 'Item Price', 'Actions']"
          :page="page" :totalItems="totalItems" 
          :rangeStart="getRangeStart()" 
          :rangeEnd="getRangeEnd()"
          :pageNumbers="pageNumbers" 
          @prev-page="prevPage"
          @next-page="nextPage"
          @go-to-page="goToPage"
          :searchItems="searchText" 
          @performSearch="performSearch" 
          @openModalAdd="openModalAdd"
          @openModalDeleted="openModalDeleted"
          >
            <tbody v-if="resultSearch.length === 0">

              <tr v-for="(item, index) in items" :key="item.id" class="border-b dark:border-gray-700">
                <th scope="row" class="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">{{ index + 1
                }}</th>
                <td class="px-4 py-3">{{ item.item_Name }}</td>
                <td class="px-4 py-3">{{ formatPrice(item.item_Price) }}</td>
                <td class="px-4 py-3 flex items-center justify-end">
                  <button type="button" @click="openModalEdit(item.id)"
                    class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                    Edit
                  </button>
                  <button type="button" @click.prevent="softDeleteItem(item.id)"
                    class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                    Delete
                  </button>
                </td>
              </tr>

            </tbody>
            <tbody v-else>

              <tr v-for="(item, index) in resultSearch" :key="item.id" class="border-b dark:border-gray-700">
                <th scope="row" class="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">{{ index + 1
                }}</th>
                <td class="px-4 py-3">{{ item.item_Name }}</td>
                <td class="px-4 py-3">{{ formatPrice(item.item_Price) }}</td>
                <td class="px-4 py-3 flex items-center justify-end">
                  <button type="button" @click="openModalEdit(item.id)"
                    class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                    Edit
                  </button>
                  <button type="button" @click.prevent="softDeleteItem(item.id)"
                    class="flex w-full items-center py-2 px-4 text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-primary-700 focus:z-10 focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700">
                    Delete
                  </button>
                </td>
              </tr>

            </tbody>
          </ReuseableTable>


  <!-- edit modal -->
  <modal v-if="isModalEditOpen" @close="closeModalEdit">
    <form class="w-full max-w-lg">
      <div class="flex flex-wrap -mx-3 mb-2">
        <div class="w-full px-3">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
            Item Name
          </label>
          <input v-model="input_item_name"
            class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            type="text">
        </div>
      </div>
      <div class="flex flex-wrap -mx-3 mb-2">
        <div class="w-full px-3">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
            Price
          </label>
          <input v-model="input_item_price"
            class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            type="text">
        </div>
      </div>
      <button @click.prevent="updateItem" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
        Update
      </button>
    </form>
  </modal>

  <!-- Add Modal -->
  <modal v-if="isModalAddOpen" @close="closeModalAdd">
    <form class="w-full max-w-lg">
      <div class="flex flex-wrap -mx-3 mb-2">
        <div class="w-full px-3">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
            Item Name
          </label>
          <input v-model="input_item_name"
            class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            type="text">
        </div>
      </div>
      <div class="flex flex-wrap -mx-3 mb-2">
        <div class="w-full px-3">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
            Price
          </label>
          <input v-model="input_item_price"
            class="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            type="text">
        </div>
      </div>
      <button @click.prevent="addItem" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
        Add
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
          <th scope="col" class="px-6 py-4">Item Price</th>
          <th scope="col" class="px-6 py-4">Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in deleteditems" :key="item.id" class="border-b dark:border-neutral-500">
          <td class="whitespace-nowrap px-6 py-4 font-medium">{{ index + 1 }}</td>
          <td class="whitespace-nowrap px-6 py-4">{{ item.item_Name }}</td>
          <td class="whitespace-nowrap px-6 py-4">{{ item.item_Price }}</td>
          <td class="whitespace-nowrap px-6 py-4">
            <button @click.prevent="restoreItem(item.id)"
              class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
              Restore
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </modal>
  <!-- end modal content -->
</template>




<script>
import axios from 'axios';
import Swal from 'sweetalert2';
import Modal from './Modal.vue';
import ReuseableTable from './Table.vue';
export default {
  components: {
    Modal,
    ReuseableTable,
  },
  data() {
    return {
      items: [],
      deleteditems: [],
      editItemId: null,
      page: 1,
      totalPage: 1,
      totalItems: 1,
      searchText: "",
      resultSearch: [],
      isModalEditOpen: false,
      isModalAddOpen: false,
      isModalDeletedOpen: false,
      input_item_name: '',
      input_item_price: '',
      headers: ["No", "Item Name", "Item Price", "Actions"],
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
          this.totalPage = Math.ceil(response.data.meta.pagination.count / 10);
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
          this.input_item_name = itemData.item_Name;
          this.input_item_price = itemData.item_Price;
        })
        .catch((error) => {
          console.error("Terjadi kesalahan:", error);
        });
      this.isModalEditOpen = true;
    },
    closeModalDeleted() {
      this.isModalDeletedOpen = false;
    },
    openModalDeleted() {
      this.isModalDeletedOpen = true;
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
    updateItem() {
      const itemId = this.editItemId;
      const itemName = this.input_item_name;
      const itemPrice = this.input_item_price;

      // Tambahkan konfirmasi Swal di sini
      Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin mengedit item ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal',
      }).then((result) => {
        if (result.isConfirmed) {
          const requestBody = {
            id: itemId,
            item_Name: itemName,
            item_Price: parseInt(itemPrice),
          };

          axios
            .put("https://localhost:5001/API/items/UpdateItem", requestBody)
            .then((response) => {
              this.closeModalEdit();
              this.fetchItems();

              // Tampilkan pesan Swal setelah berhasil mengedit item
              Swal.fire('Sukses', 'Item berhasil di edit', 'success');
            })
            .catch((error) => {
              console.error("Failed to update item:", error);
            });
        }
      });
    },
    addItem() {
      const itemName = this.input_item_name;
      const itemPrice = this.input_item_price;

      // Membuat objek yang akan dikirim sebagai payload
      const requestBody = {
        Item_Name: itemName,
        Item_Price: parseFloat(itemPrice),
      };

      // Tampilkan konfirmasi menggunakan Swal
      Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin menambahkan item ini?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal'
      }).then((result) => {
        if (result.isConfirmed) {
          axios
            .post("https://localhost:5001/API/items/AddItem", requestBody)
            .then((response) => {
              // Handle respon sukses
              this.closeModalAdd();
              this.fetchItems();

              // Tampilkan pesan sukses menggunakan Swal
              Swal.fire({
                title: 'Sukses',
                text: 'Item berhasil ditambahkan',
                icon: 'success',
              });
            })
            .catch((error) => {
              console.error("Gagal menambahkan item:", error);
            });
        }
      });
    },
    softDeleteItem(itemId) {
      const item_Id = itemId;

      // Tambahkan konfirmasi Swal di sini
      Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin menghapus item ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal',
      }).then((result) => {
        if (result.isConfirmed) {
          const requestBody = {
            id: item_Id
          };

          axios
            .put("https://localhost:5001/API/items/DeleteItem", requestBody)
            .then((response) => {
              this.fetchItems(); // Mengambil data lagi setelah item dihapus
              this.DeletedItems();
              // Tampilkan pesan Swal setelah berhasil menghapus item
              Swal.fire('Sukses', 'Item berhasil dihapus', 'success');
            })
            .catch((error) => {
              console.error("Failed to soft delete item:", error);
            });
        }
      });
    },
    restoreItem(itemId) {
      const item_Id = itemId;

      // Tambahkan konfirmasi Swal di sini
      Swal.fire({
        title: 'Konfirmasi',
        text: 'Apakah Anda yakin ingin mengembalikan item ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ya',
        cancelButtonText: 'Batal',
      }).then((result) => {
        if (result.isConfirmed) {
          const requestBody = {
            id: item_Id
          };

          axios
            .put("https://localhost:5001/API/items/RestoreItem", requestBody)
            .then((response) => {
              this.fetchItems(); // Mengambil data lagi setelah item direstore
              this.DeletedItems();

              // Tampilkan pesan Swal setelah berhasil merestore item
              Swal.fire('Sukses', 'Item berhasil direstore', 'success');
            })
            .catch((error) => {
              console.error("Gagal merestore item:", error);
            });
        }
      });
    },
    performSearch(searchText) {
      this.searchText=searchText;
      axios
        .get(`https://localhost:5001/API/items/search/${this.searchText}`)
        .then((response) => {
          // Handle the search results here
          this.resultSearch = response.data;
        })
        .catch((error) => {
          console.error(error);
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

<style scoped></style>
