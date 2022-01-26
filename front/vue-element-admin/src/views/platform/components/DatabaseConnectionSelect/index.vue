<template>
  <el-select v-model="valueIds" :placeholder="$t('components.请选择')">
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
import { getAll } from '@/api/platform/database-connection'

export default {
  name: 'DatabaseConnectionSelect',
  props: {
    value: {
      required: false,
      type: String,
      default: ''
    },
    tenantId: {
      required: false,
      type: String,
      default: ''
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
        this.options = response.data.items.filter(f => {
          if (this.tenantId) {
            return f.rangeTenantIds == null || f.rangeTenantIds.length === 0 || f.rangeTenantIds.indexOf(this.tenantId) > -1
          }
          return f.rangeTenantIds == null || f.rangeTenantIds.length === 0
        }).map(m => { return { value: m.name, label: m.name, disabled: m.status !== 1 } })
      })
    }
  }
}
</script>
