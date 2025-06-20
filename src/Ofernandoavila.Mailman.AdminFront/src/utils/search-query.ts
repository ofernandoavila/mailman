export function buildSearchQuery(data: Record<string, any>) : string {
    const params = new URLSearchParams();

    for(const [key, value] of Object.entries(data)) {
        if(value !== undefined && value !== null && value !== '' && value !== false)
            params.append(key, value);
    }

    return params.toString();
}