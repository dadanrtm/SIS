// 1. Import DurableObject dari Cloudflare
import { DurableObject } from "cloudflare:workers";

// 2. Ekspor class ApiSIS sesuai dengan yang ada di wrangler.jsonc
export class ApiSIS extends DurableObject {
  // Class ini hanya perlu ada, tidak perlu logika fetch
  // karena request akan diarahkan ke container oleh binding
}

// 3. Kode default untuk meneruskan request ke container
export default {
  async fetch(request, env) {
    // Pastikan nama binding (MY_API_CONTAINER) sesuai dengan wrangler.jsonc
    return env.MY_API_CONTAINER.fetch(request);
  }
}