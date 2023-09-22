import { createApp } from 'vue'
import App from './App.vue'
import '../src/assets/style.css'
import Router from './routes'


createApp(App).use(Router).mount('#app')
