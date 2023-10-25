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
        <tr v-for="(item, index) in deletedItems" :key="item.id" class="border-b dark:border-neutral-500">
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
<script setup>
import axios from 'axios';

import { ref, computed} from 'vue';
import Swal from 'sweetalert2';
import Modal from './Modal.vue';
import ReuseableTable from './Table.vue';

const items = ref([]);
const deletedItems = ref([]);
const editItemId = ref(null);
const page = ref(1);
const totalPage = ref(1);
const totalItems = ref(1);
const searchText = ref("");
const resultSearch = ref([]);
const isModalEditOpen = ref(false);
const isModalAddOpen = ref(false);
const isModalDeletedOpen = ref(false);
const inputItemName = ref('');
const inputItemPrice = ref('');
const headers = ["No", "Item Name", "Item Price", "Actions"];



const fetchItems = async () => {
  try {
    const response = await axios.get(`https://localhost:5001/API/items/getallitem?page=${page.value}&pageSize=10`);
    items.value = response.data.data;
    totalPage.value = Math.ceil(response.data.meta.pagination.count / 10);
    totalItems.value = response.data.meta.pagination.count;
  } catch (error) {
    console.error("Terjadi kesalahan:", error);
  }
};
fetchItems();
const DeletedItems = async () => {
  try {
    const response = await axios.get("https://localhost:5001/API/items/getalldeleted");
    deletedItems.value = response.data.data;
  } catch (error) {
    console.error("Terjadi kesalahan:", error);
  }
};

DeletedItems();


const formatPrice = (price) => {
  return new Intl.NumberFormat("id-ID", {
    style: "currency",
    currency: "IDR",
    minimumFractionDigits: 2,
  }).format(price);
};

const openModalEdit = (itemId) => {
  editItemId.value = itemId;
  axios.get(`https://localhost:5001/API/items/getitembyid/${itemId}`)
    .then((response) => {
      const itemData = response.data[0];
      inputItemName.value = itemData.item_Name;
      inputItemPrice.value = itemData.item_Price;
    })
    .catch((error) => {
      console.error("Terjadi kesalahan:", error);
    });
  isModalEditOpen.value = true;
};

const closeModalDeleted = () => {
  isModalDeletedOpen.value = false;
};

const openModalDeleted = () => {
  isModalDeletedOpen.value = true;
};

const closeModalEdit = () => {
  isModalEditOpen.value = false;
};

const openModalAdd = () => {
  isModalAddOpen.value = true;
};

const closeModalAdd = () => {
  isModalAddOpen.value = false;
};

const updateItem = () => {
  const itemId = editItemId.value;
  const itemName = inputItemName.value;
  const itemPrice = inputItemPrice.value;

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
        item_Price: parseFloat(itemPrice),
      };

      axios.put("https://localhost:5001/API/items/UpdateItem", requestBody)
        .then((response) => {
          closeModalEdit();
          fetchItems();

          Swal.fire('Sukses', 'Item berhasil di edit', 'success');
        })
        .catch((error) => {
          console.error("Failed to update item:", error);
        });
    }
  });
};

const addItem = () => {
  const itemName = inputItemName.value;
  const itemPrice = inputItemPrice.value;

  const requestBody = {
    Item_Name: itemName,
    Item_Price: parseFloat(itemPrice),
  };

  Swal.fire({
    title: 'Konfirmasi',
    text: 'Apakah Anda yakin ingin menambahkan item ini?',
    icon: 'question',
    showCancelButton: true,
    confirmButtonText: 'Ya',
    cancelButtonText: 'Batal'
  }).then((result) => {
    if (result.isConfirmed) {
      axios.post("https://localhost:5001/API/items/AddItem", requestBody)
        .then((response) => {
          closeModalAdd();
          fetchItems();

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
};

const softDeleteItem = (itemId) => {
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
        id: itemId
      };

      axios.put("https://localhost:5001/API/items/DeleteItem", requestBody)
        .then((response) => {
          fetchItems();
          DeletedItems();
          Swal.fire('Sukses', 'Item berhasil dihapus', 'success');
        })
        .catch((error) => {
          console.error("Failed to soft delete item:", error);
        });
    }
  });
};

const restoreItem = (itemId) => {
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
        id: itemId
      };

      axios.put("https://localhost:5001/API/items/RestoreItem", requestBody)
        .then((response) => {
          fetchItems();
          DeletedItems();

          Swal.fire('Sukses', 'Item berhasil direstore', 'success');
        })
        .catch((error) => {
          console.error("Gagal merestore item:", error);
        });
    }
  });
};

const performSearch = (searchText) => {
  searchText.value = searchText;
  axios.get(`https://localhost:5001/API/items/search/${searchText}`)
    .then((response) => {
      resultSearch.value = response.data;
    })
    .catch((error) => {
      console.error(error);
    });
};

const pageNumbers = computed(() => {
  const pages = [];
  for (let i = 1; i <= totalPage.value; i++) {
    pages.push(i);
  }
  return pages;
});

const nextPage = () => {
  page.value++;
  fetchItems();
};

const prevPage = () => {
  if (page.value > 1) {
    page.value--;
    fetchItems();
  }
};

const goToPage = (pageNumber) => {
  if (pageNumber >= 1 && pageNumber <= totalPage.value) {
    page.value = pageNumber;
    fetchItems();
  }
};

const getRangeStart = () => {
  return (page.value - 1) * 10 + 1;
};

const getRangeEnd = () => {
  const end = page.value * 10;
  return end > totalItems.value ? totalItems.value : end;
};

</script>