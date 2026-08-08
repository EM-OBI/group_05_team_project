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
    }

    return { ok: response.ok, status: response.status, data: result };
}

export async function getJson(url) {
    const response = await fetch(url, {
        method: 'GET',
        headers: { 'Accept': 'application/json' }
    });

    let result = null;
    try {
        result = await response.json();
    } catch {
    }

    return { ok: response.ok, status: response.status, data: result };
}

export async function getJsonData(url) {
    const response = await fetch(url, {
        method: 'GET',
        headers: { 'Accept': 'application/json' }
    });

    if (!response.ok) {
        return null;
    }

    try {
        return await response.json();
    } catch {
        return null;
    }
}
