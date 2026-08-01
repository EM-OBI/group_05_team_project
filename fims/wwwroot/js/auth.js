export async function postJson(url, data) {
    const response = await fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });

    let result = null;
    try {
        result = await response.json();
    } catch {
        // no JSON body
    }

    return { ok: response.ok, status: response.status, data: result };
}
