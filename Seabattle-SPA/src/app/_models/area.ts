import { Ship } from './ship';
import { ShipData } from './shipData';

export class Area {
    private matrix: number[][];
    public ships: Ship[];

    constructor() {
        this.refreshShipsLocation();
    }

    public refreshShipsLocation() {
        this.ships = new Array<Ship>();
        this.locatedShips();
    }

    public drawShips(pixelToOneStep: number, ctx: CanvasRenderingContext2D) {
      for (const ship of this.ships) {
        ship.drawShip(pixelToOneStep, ctx);
      }
    }

    private locatedShips() {
        let decks = 4;
        let shipData: ShipData;
        this.matrix = this.createMatrix();

        for (let i = 1; i <= 4; i++) {
          for (let j = 0; j < i; j++) {
            shipData = this.getCoordinatesDecks(decks);
            this.ships.push(this.createShip(shipData));
          }
          decks--;
        }
    }

    private getCoordinatesDecks(decks: number): ShipData {
        const kx = this.getRandom(1);
        const ky = kx === 0 ? 1 : 0;
        const x = kx === 0 ? this.getRandom(9) : this.getRandom(10 - decks);
        const y = kx === 0 ? this.getRandom(10 - decks) : this.getRandom(9);

        if (!this.checkLocationShip(x, y, kx, ky, decks)) {
          return this.getCoordinatesDecks(decks);
        }

        const data: ShipData = { x, y, kx, ky, decks };

        return data;
    }

    private checkLocationShip(x: number, y: number, kx: number, ky: number, decks: number): boolean {
        let toX, toY;

        const fromX = (x === 0) ? x : x - 1;
        if (x + kx * decks === 10 && kx === 1) {
            toX = x + kx * decks;
        }
        if (x + kx * decks < 10 && kx === 1) {
            toX = x + kx * decks + 1;
        }
        if (x === 9 && kx === 0) {
            toX = x + 1;
        }
        if (x < 9 && kx === 0) {
            toX = x + 2;
        }

        const fromY = (y === 0) ? y : y - 1;
        if (y + ky * decks === 10 && ky === 1)  {
            toY = y + ky * decks;
        }
        if (y + ky * decks < 10 && ky === 1) {
            toY = y + ky * decks + 1;
        }
        if (y === 9 && ky === 0) {
            toY = y + 1;
        }
        if (y < 9 && ky === 0) {
            toY = y + 2;
        }

        for (let i = fromX; i < toX; i++) {
          for (let j = fromY; j < toY; j++) {
            if (this.matrix[i][j] === 1) {
              return false;
            }
          }
        }
        return true;
    }

    private createShip(shipData: ShipData): Ship {
        let k = 0;
        const ship = new Ship();
        while (k < shipData.decks) {
          this.matrix[shipData.x + k * shipData.kx][shipData.y + k * shipData.ky] = 1;
          ship.Cells.push({x: shipData.x + k * shipData.kx, y: shipData.y + k * shipData.ky, isAlive: true});
          k++;
        }
        ship.IsHorizontal = shipData.kx === 1;
        return ship;
    }

    private createMatrix(): number[][] {
        const arr: number[][] = new Array();
        for (let i = 0; i < 10; i++) {
          arr[i] = [];
          for (let j = 0; j < 10; j++) {
            arr[i][j] = 0;
          }
        }
        return arr;
    }

    private getRandom(n: number) {
        return Math.floor(Math.random() * (n + 1));
    }
}
