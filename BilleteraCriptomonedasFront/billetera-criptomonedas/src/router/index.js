import ComprarCripto from '@/views/comprarCripto.vue';
import NuevoCliente from '@/views/nuevoCliente.vue';
import HistorialTransacciones from '@/views/HistorialTransacciones.vue';
import { createRouter, createWebHistory } from 'vue-router';


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path:'/nuevoCliente',
      name: 'nuevoCliente',
      component: NuevoCliente
    },
    {
      path: '/comprarCriptomonedas',
      name: 'comprarCriptomonedas',
      component: ComprarCripto
    },
    {
      path:'/historialTransacciones',
      name:'historialTransacciones',
      component: HistorialTransacciones
    }
  ],
})

export default router
