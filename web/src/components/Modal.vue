<template>
  <div class="modal" @click="closeModal">
    <div class="modal-content" @click.stop>
      <button class="close-button"></button>
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  methods: {
    closeModal(event) {
      if (event.target.classList.contains('modal')) {
        this.$emit('close');
      }
    }
  }
}
</script>

<style scoped>
.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 999; /* Nilai z-index yang lebih tinggi daripada komponen lain */
}

.modal-content {
  background-color: white;
  padding: 20px;
  border-radius: 5px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.3);
  z-index: 1000; /* Nilai z-index yang lebih tinggi dari modal */
}

</style>
