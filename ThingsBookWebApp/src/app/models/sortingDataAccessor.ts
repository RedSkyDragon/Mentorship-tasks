export function SortingDataAccessor(item, property) {
    if (property.indexOf('.') !== -1) {
      const ids = property.split('.');
      return item[ids[0]][ids[1]];
    } else {
      return item[property];
    }
}
