import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MaritimeService } from './maritime.service';

describe('MaritimeService', () => {
  let service: MaritimeService;
  let httpMock: HttpTestingController;
  const apiUrl = 'http://localhost:5288/api';

  // Mock data
  const mockShip = { id: 1, name: 'Ocean Queen', maxSpeed: 28 };
  const mockShips = [mockShip];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MaritimeService]
    });

    service = TestBed.inject(MaritimeService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getShips', () => {
    it('should fetch all ships via GET', () => {
      service.getShips().subscribe(ships => {
        expect(ships).toEqual(mockShips);
      });

      const req = httpMock.expectOne(`${apiUrl}/ships`);
      expect(req.request.method).toBe('GET');
      req.flush(mockShips);
    });

    it('should handle server errors for getShips', () => {
      service.getShips().subscribe({
        next: () => fail('should have failed'),
        error: (error) => {
          expect(error.status).toBe(500);
        }
      });

      const req = httpMock.expectOne(`${apiUrl}/ships`);
      req.flush('Server Error', { status: 500, statusText: 'Server Error' });
    });
  });

  describe('getShip', () => {
    it('should fetch a single ship via GET', () => {
      const shipId = 1;
      service.getShip(shipId).subscribe(ship => {
        expect(ship).toEqual(mockShip);
      });

      const req = httpMock.expectOne(`${apiUrl}/ships/${shipId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockShip);
    });

    it('should handle 404 for non-existent ship', () => {
      const shipId = 999;
      service.getShip(shipId).subscribe({
        next: () => fail('should have failed'),
        error: (error) => {
          expect(error.status).toBe(404);
        }
      });

      const req = httpMock.expectOne(`${apiUrl}/ships/${shipId}`);
      req.flush('Not Found', { status: 404, statusText: 'Not Found' });
    });
  });

  describe('createShip', () => {
    it('should create a ship via POST', () => {
      const newShip = { name: 'New Ship', maxSpeed: 30 };
      service.createShip(newShip).subscribe(ship => {
        expect(ship).toEqual(mockShip);
      });

      const req = httpMock.expectOne(`${apiUrl}/ships`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(newShip);
      req.flush(mockShip);
    });

    it('should handle validation errors for createShip', () => {
      const invalidShip = { name: '', maxSpeed: -5 };
      service.createShip(invalidShip).subscribe({
        next: () => fail('should have failed'),
        error: (error) => {
          expect(error.status).toBe(400);
        }
      });

      const req = httpMock.expectOne(`${apiUrl}/ships`);
      req.flush('Bad Request', { status: 400, statusText: 'Bad Request' });
    });
  });

  describe('updateShip', () => {
    it('should update a ship via PUT', () => {
      const updatedShip = { ...mockShip, maxSpeed: 32 };
      service.updateShip(updatedShip.id, updatedShip).subscribe(ship => {
        expect(ship).toEqual(updatedShip);
      });

      const req = httpMock.expectOne(`${apiUrl}/ships/${updatedShip.id}`);
      expect(req.request.method).toBe('PUT');
      expect(req.request.body).toEqual(updatedShip);
      req.flush(updatedShip);
    });

    it('should handle ID mismatch for updateShip', () => {
      const mismatchedShip = { id: 1, name: 'Ship', maxSpeed: 30 };
      service.updateShip(2, mismatchedShip).subscribe({
        next: () => fail('should have failed'),
        error: (error) => {
          expect(error).toBeTruthy();
        }
      });

      // No HTTP expectation needed as it should fail before making the request
    });
  });

  describe('deleteShip', () => {
    it('should delete a ship via DELETE', () => {
      const shipId = 1;
      service.deleteShip(shipId).subscribe(response => {
        expect(response).toBeNull(); // Typically DELETE returns empty body
      });

      const req = httpMock.expectOne(`${apiUrl}/ships/${shipId}`);
      expect(req.request.method).toBe('DELETE');
      req.flush(null);
    });
  });
});