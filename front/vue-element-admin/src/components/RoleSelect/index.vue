<template>
  <el-select v-model="valueIds" multiple :placeholder="$t('components.请选择')">
    <el-option
      v-for="item in options"
      :key="item.value"
      :label="item.label"
      :value="item.value"
      :disabled="item.disabled"
    />
  </el-select>
</template>

<script>
import { getAll } from '@/api/system/role'

export default {
  name: 'RoleSelect',
  props: {
    value: {
      required: false,
      type: Array,
      default: () => []
    }
  },
  data() {
    return {
      options: []
    }
  },
  computed: {
    valueIds: {
      get() {
        return this.value
      },
      set(val) {
        this.$emit('input', val)
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      getAll().then(response => {
        this.options = response.data.items.map(m => { return { value: m.id, label: m.name, disabled: m.status !== 1 } })
      })
    }
  }
}
</script>
