<template>
  <el-select v-model="valueIds" multiple :placeholder="placeholder">
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
import { getAll } from '@/api/platform/tenant'
import i18n from '@/lang'

export default {
  name: 'TenantMultipleSelect',
  props: {
    value: {
      required: false,
      type: Array,
      default: () => []
    },
    placeholder: {
      required: false,
      type: String,
      default: i18n.t('components.请选择')
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
