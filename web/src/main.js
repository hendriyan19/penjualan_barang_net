import { createApp } from 'vue'
import App from './App.vue'
import '../src/assets/style.css'
import Router from './routes'
import { createPinia } from 'pinia'

const app = createApp(App);

const pinia = createPinia();

app.use(pinia);
app.use(Router).mount('#app'); 
