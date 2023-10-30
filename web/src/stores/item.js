import axios from 'axios';
import { defineStore } from 'pinia';

export const useStore = defineStore({
  id: 'myStore',
  state: () => ({
    items: [],
    totalPage:0,
    totalItems:0,
  }),
  getters: {
    getItems() {
      return this.items;
    },
    getTotalPage() {
        return this.totalPage;
      },
    getTotalItems(){
        return this.totalItems;
    }
  },
  actions: {
    async fetchItems(page) {
      try {
        const response = await axios.get(`https://localhost:5001/API/items/getallitem?page=${page.value}&pageSize=10`);
        this.items = response.data.data;
        this.totalPage = Math.ceil(response.data.meta.pagination.count / 10);
        this.totalItems = response.data.meta.pagination.count;
      } catch (error) {
        console.error("Terjadi kesalahan:", error);
      }
    },
  },
});
