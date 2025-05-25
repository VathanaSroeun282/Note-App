import { createRouter, createWebHistory } from 'vue-router'
import NoteView from '../views/NoteView.vue'

const routes = [
  { path: '/', component: NoteView },
]

export default createRouter({
  history: createWebHistory(),
  routes,
});