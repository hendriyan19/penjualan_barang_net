import {createWebHistory, createRouter} from "vue-router";
 
import Home from "../components/Home.vue";
import Item from "../components/Item.vue";
import Order from "../components/Order.vue";

const routes = [
    {
        path:"/",
        name:"Home",
        component:Home
    },
    {
        path:"/item",
        name:"Item",
        component:Item
    },
    {
        path:"/order",
        name:"Order",
        component:Order
    },
]
const router = createRouter({
    history:createWebHistory(),
    routes
})

export default router;